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
    [SerializeField] private float grappleCooldown = 3f;
    [SerializeField] private float freezeCooldown = 5f;
    [SerializeField] private float flameCooldown = 5f;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawn;
    [SerializeField] private float shootingForce;
    [SerializeField] private LineRenderer line;
    [SerializeField] private bool freezeEnabled = false;
    [SerializeField] private bool flameEnabled = false;
    [SerializeField] private bool grappleEnabled = false;

    private static PlayerController instance = null;
    private AudioSource audioSource;
    private Vector3 lastPos;
    private bool justShot;
    private bool canGrapple;
    private bool canFreeze;
    private bool canFlame;
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
        audioSource = GetComponent<AudioSource>();
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
        arrowType = ArrowType.Standard;

        justShot = false;
        canGrapple = true;
        canFreeze = true;
        canFlame = true;
        isGrounded = true;
        isAiming = false;
        isGrapplingTo = false;
        isGrapplingFrom = false;
        if (freezeEnabled) UnlockFreeze();
        if (flameEnabled) UnlockFlame();
        if (grappleEnabled) UnlockGrapple();
    }

    // Update is called once per frame
    private void Update() {
        ApplyMovement(); // Applies gravity, do not put physics code before this.
        isAiming = (Input.GetMouseButton(1) && isGrounded);
        //print("y " + player.position.y);

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
        } else if (Input.GetKeyDown(KeyCode.Alpha2) && grappleEnabled && canGrapple) {
            arrowType = ArrowType.Grapple;
            UIManager.getInstance().SetActive(ArrowType.Grapple);
            print(arrowType);
        } else if (Input.GetKeyDown(KeyCode.Alpha4) && freezeEnabled && canFreeze) {
            arrowType = ArrowType.Freeze;
            UIManager.getInstance().SetActive(ArrowType.Freeze);
            print(arrowType);
        } else if (Input.GetKeyDown(KeyCode.Alpha3) && flameEnabled && canFlame) {
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
                float dist = Vector3.Distance(player.position, grapplePoint);
                Vector3 dif = (grapplePoint - player.position) / dist * 15f * Time.deltaTime;
                controller.Move(dif);
                //player.position = Vector3.Lerp(player.position, grapplePoint, 5f * Time.deltaTime);
                //print("Distance: " + dist);
                dist = Vector3.Distance(player.position, grapplePoint);
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
            if (!isGrounded) velocity.y += gravity * Time.deltaTime;
        } else {
            Vector3 move = transform.right * x + transform.forward * z; 
            controller.Move(move * (speed/3) * Time.deltaTime);
        }
        

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask.value);
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        } else if (isGrounded && velocity.y == -2f) {
            velocity.y = 0;
        }
        //print("vel y: " + velocity.y);
        controller.Move(velocity * Time.deltaTime);
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

    public void StartFreezeCooldown() {
        UIManager ui = UIManager.getInstance();
        ui.HideFreezeIcon();
        arrowType = ArrowType.Standard;
        ui.SetActive(ArrowType.Standard);
        canFreeze = false;
        ui.FadeInFreezeIcon(freezeCooldown);
        StartCoroutine(WaitTimeForFreeze());
    }

    public void StartFlameCooldown()
    {
        UIManager ui = UIManager.getInstance();
        ui.HideFlameIcon();
        arrowType = ArrowType.Standard;
        ui.SetActive(ArrowType.Standard);
        canFlame = false;
        ui.FadeInFlameIcon(flameCooldown);
        StartCoroutine(WaitTimeForFlame());
    }

    public void UnlockFreeze() {
        freezeEnabled = true;
        UIManager.getInstance().EnableFreeze();
    }

    public void UnlockFlame() {
        flameEnabled = true;
        UIManager.getInstance().EnableFlame();
    }

    public void UnlockGrapple() {
        grappleEnabled = true;
        UIManager.getInstance().EnableGrapple();
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

    IEnumerator WaitTimeForFreeze() {
        yield return new WaitForSeconds(freezeCooldown);
        canFreeze = true;
    }

    IEnumerator WaitTimeForFlame()
    {
        yield return new WaitForSeconds(flameCooldown);
        canFlame = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (isGrapplingTo && hit.gameObject.layer != 9 && hit.gameObject.tag != "Player" && hit.gameObject.tag != "Arrow")
        {
            print("PLAYER COLLIDE WITH: " + hit.gameObject.name);
            isGrapplingTo = false;
            line.positionCount = 0;
            canGrapple = false;
            grapplePoint = Vector3.zero;
        }
    }
}
