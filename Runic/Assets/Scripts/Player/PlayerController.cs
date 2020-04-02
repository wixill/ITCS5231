using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform player;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Camera cam;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawn;
    [SerializeField] private float shootingForce;

    private Vector3 lastPos;
    private Vector3 startCamPos;
    private bool justShot = false;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isAiming;

    // Start is called before the first frame update
    private void Start() {
        lastPos = new Vector3(0, 0, 0);
        startCamPos = new Vector3(0, 1.868f, 0.273f);
    }

    // Update is called once per frame
    private void Update() {
        ApplyMovement(); // Applies gravity, do not put physics code before this.

        isAiming = (Input.GetMouseButton(1) && isGrounded);
        Vector3 pos = player.transform.localPosition;
        if (isGrounded) {
            anim.SetBool("isGrounded", true);
            anim.SetFloat("VelocityY", 0);
            anim.SetBool("isAiming", isAiming);

            if (Input.GetButtonDown("Jump") && !isAiming) velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            if (pos.Equals(lastPos))
            {
                anim.SetBool("isWalking", false);
            }
            else
            {
                anim.SetBool("isWalking", true);
                anim.SetFloat("VelocityX", Input.GetAxis("Horizontal"));
                anim.SetFloat("VelocityZ", Input.GetAxis("Vertical"));
            }
        } else {
            anim.SetBool("isGrounded", false);
            anim.SetFloat("VelocityY", velocity.y);
        }
        lastPos.Set(pos.x, pos.y, pos.z);

        if (Input.GetMouseButtonDown(0) && isAiming)
        {
            //if we did not just shoot an arrow or we waited 7 seconds to shoot again
            if (justShot == false)
            {
                ShootArrow();
                justShot = true;
            }
            else
            { //if we just shot an arrow
                //cooldown time to wait before shooting again 
                StartCoroutine(WaitTimeForShooting());
                //allows the player to shoot again after cooldown
            }
        }
    }

    /**
     * Applies controller input to the player transform and applies gravity/checks
     * if the player is grounded or not.
     */
    private void ApplyMovement() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask.value);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
    }

    /**
     * Creates a new arrow and shoots it in the direction the camera is looking.
     */
    private void ShootArrow() {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.velocity = cam.transform.forward * shootingForce;
    }

    public bool isPlayerAiming() {
        return isAiming;
    }

    //cooldown time for the shooting, set to 3 seconds to wait
    IEnumerator WaitTimeForShooting()
    {
        new WaitForSeconds(3);
        yield return justShot = false;
    }
}
