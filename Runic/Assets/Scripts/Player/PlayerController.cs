using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] private Renderer model;
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform player;
    [SerializeField] private SkinnedMeshRenderer bow;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Camera cam;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float speed = 12f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float grappleCooldown = 8f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawn;
    [SerializeField] private float shootingForce;
    [SerializeField] private LineRenderer line;

    private static PlayerController instance = null;
    private Vector3 lastPos;
    private Vector3 startCamPos;
    private bool justShot;
    private bool canGrapple;
    private Vector3 velocity;
    private bool isGrounded;
    private bool isAiming;
    private bool isGrapplingTo;
    private bool isGrapplingFrom;
    private ArrowType arrowType;
    private Vector3 grapplePoint;
    private GameObject objectToPull;

    private void Awake()
    {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    public static PlayerController getInstance() {
        return instance;
    }

    // Start is called before the first frame update
    private void Start() {
        grapplePoint = Vector3.zero;
        lastPos = Vector3.zero;
        startCamPos = new Vector3(0, 1.868f, 0.273f);
        arrowType = ArrowType.Standard;
        justShot = false;
        canGrapple = true;
        isGrounded = true;
        isAiming = false;
        isGrapplingTo = false;
        isGrapplingFrom = false;
    }

    // Update is called once per frame
    private void Update() {
        ApplyMovement(); // Applies gravity, do not put physics code before this.
        isAiming = (Input.GetMouseButton(1) && isGrounded);

        if (isAiming) {
            Color tempColor = bow.material.color;
            tempColor.a = 0.6f;
            bow.material.color = tempColor;
        } else {
            Color tempColor = bow.material.color;
            tempColor.a = 1f;
            bow.material.color = tempColor;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            arrowType = ArrowType.Standard;
            UIManager.getInstance().SetActive(ArrowType.Standard);
            print(arrowType);
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && canGrapple) {
            arrowType = ArrowType.Grapple;
            UIManager.getInstance().SetActive(ArrowType.Grapple);
            print(arrowType);
        } else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            arrowType = ArrowType.Freeze;
            UIManager.getInstance().SetActive(ArrowType.Freeze);
            print(arrowType);
        } else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            arrowType = ArrowType.Flame;
            UIManager.getInstance().SetActive(ArrowType.Flame);
            print(arrowType);
        }

        if (isGrapplingTo || isGrapplingFrom) {
            // Pulling the player to an object
            if (isGrapplingTo) {
                Vector3[] positions = { model.bounds.center, grapplePoint };
                line.SetPositions(positions);
                isAiming = false;
                player.position = Vector3.Lerp(player.position, grapplePoint, 5f * Time.deltaTime);
                float dist = Vector3.Distance(player.position, grapplePoint);
                //print("Distance: " + dist);
                if (dist < 3) {
                    isGrapplingTo = false;
                    line.positionCount = 0;
                    canGrapple = false;
                }
            // Pulling a grapple-able object to the player
            } else if (isGrapplingFrom && objectToPull != null) {
                Vector3[] positions = { model.bounds.center, objectToPull.transform.position };
                line.SetPositions(positions);
                objectToPull.transform.position = Vector3.Lerp(objectToPull.transform.position, model.bounds.center, 4f * Time.deltaTime);
                float dist = Vector3.Distance(objectToPull.transform.position, model.bounds.center);
                if (dist < 2) {
                    StopPullingObject();
                }
            }
        }



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
            if (!justShot)
            {
                ShootArrow();
                if (arrowType == ArrowType.Standard) {
                    UIManager.getInstance().HideStandardIcon();
                    UIManager.getInstance().FadeInStandardIcon(1f);
                }
                justShot = true;
                StartCoroutine(WaitTimeForShooting()); ;
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

        if (!isGrapplingTo) {
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * speed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask.value);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;
    }

    /**
     * Creates a new arrow and shoots it in the direction the camera is looking.
     */
    private void ShootArrow() {
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        ArrowScript arrow = newArrow.GetComponent<ArrowScript>();
        arrow.setType(arrowType);
        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.velocity = cam.transform.forward * shootingForce;
    }

    public bool IsPlayerAiming() {
        return isAiming;
    }

    public Vector3 getPlayerPos() {
        return lastPos;
    }

    public void StartGrappleTo(Vector3 toPoint) {
        print("GRAPPLIN!");
        UIManager.getInstance().HideGrappleIcon();
        arrowType = ArrowType.Standard;
        UIManager.getInstance().SetActive(ArrowType.Standard);
        line.positionCount = 2;
        isGrapplingTo = true;
        grapplePoint = new Vector3(toPoint.x, toPoint.y, toPoint.z);
        UIManager.getInstance().FadeInGrappleIcon(grappleCooldown);
        StartCoroutine(WaitTimeForGrapple());
    }

    public void StartGrappleFrom(GameObject toPull) {
        print("PULLIN!");
        UIManager.getInstance().HideGrappleIcon();
        arrowType = ArrowType.Standard;
        UIManager.getInstance().SetActive(ArrowType.Standard);
        line.positionCount = 2;
        isGrapplingFrom = true;
        this.objectToPull = toPull;
        UIManager.getInstance().FadeInGrappleIcon(grappleCooldown);
        StartCoroutine(WaitTimeForGrapple());
    }

    public void StopPullingObject()
    {
        objectToPull.GetComponent<ArrowInteraction>().setIsBeingPulled(false);
        isGrapplingFrom = false;
        line.positionCount = 0;
        canGrapple = false;
    }

    //cooldown time for the shooting, set to 3 seconds to wait
    IEnumerator WaitTimeForShooting()
    {
        yield return new WaitForSeconds(1f);
        justShot = false;
    }

    IEnumerator WaitTimeForGrapple()
    {
        yield return new WaitForSeconds(grappleCooldown);
        canGrapple = true; ;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGrapplingTo && other.gameObject.layer != 9 && other.gameObject.tag != "Player" && other.gameObject.tag != "Arrow")
        {
            print("PLAYER COLLIDE WITH: " + other.gameObject.name);
            isGrapplingTo = false;
            line.positionCount = 0;
            canGrapple = false;
            grapplePoint = Vector3.zero;
        }
    }
}
