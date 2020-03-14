using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Transform camera;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    private Vector3 lastPos;
    private Vector3 startCamPos;
    public GameObject arrowPrefab;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float shootingForce;
    private bool justShot = false;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isAiming;

    // Start is called before the first frame update
    private void Start()
    {
        lastPos = new Vector3(0, 0, 0);
        startCamPos = new Vector3(0, 1.868f, 0.273f);
    }

    // Update is called once per frame
    private void Update()
    {
        //if the left button of the mouse is held down
        if (Input.GetMouseButtonDown(0))
        {
            //if we did not just shoot an arrow or we waited 7 seconds to shoot again
            if (justShot == false)
            {
                Shoot();
                justShot = true;
            }
            //if we just shot an arrow
            else
            {
                //cooldown time to wait before shooting again 
                StartCoroutine(WaitTimeForShooting());
                //allows the player to shoot again after cooldown
                justShot = false;
            }
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        Vector3 pos = player.transform.localPosition;
        if (isGrounded) {
            anim.SetBool("isGrounded", true);
            anim.SetFloat("VelocityY", 0);
            bool aiming = Input.GetMouseButton(1);
            anim.SetBool("isAiming", aiming);

            if (Input.GetButtonDown("Jump")) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            

            if (pos.Equals(lastPos))
            {
                anim.SetBool("isWalking", false);
                //Debug.Log(startCamPos.ToString());
                camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, startCamPos, 6f * Time.deltaTime);
                camera.transform.position.Set(startCamPos.x, startCamPos.y, startCamPos.z);
            }
            else
            {
                anim.SetBool("isWalking", true);
                if (!aiming) {
                    camera.transform.position.Set(startCamPos.x, 1.489f, 0.542f);
                    camera.transform.localPosition = Vector3.Lerp(camera.transform.localPosition, new Vector3(startCamPos.x, 1.489f, 0.542f), 6f * Time.deltaTime);
                }
                anim.SetFloat("VelocityX", Input.GetAxis("Horizontal"));
                anim.SetFloat("VelocityZ", Input.GetAxis("Vertical"));
            }
        } else {
            anim.SetBool("isGrounded", false);
            anim.SetFloat("VelocityY", velocity.y);
        }
        lastPos.Set(pos.x, pos.y, pos.z);
    }
    void Shoot()
    {
        GameObject a = Instantiate(arrowPrefab);
        a.transform.position = new Vector3(player.transform.position.x, 2f, player.transform.position.z);
        rb = a.GetComponent<Rigidbody>();
        rb.velocity = camera.transform.forward * shootingForce;
    }

    //cooldown time for the shooting, set to 7 seconds to wait
    IEnumerator WaitTimeForShooting()
    {
        yield return new WaitForSeconds(5);
    }
}
