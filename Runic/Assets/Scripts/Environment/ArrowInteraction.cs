using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class ArrowInteraction : MonoBehaviour

{
    [Header("Interaction Behaviors")]
    // Can be broken by a normal arrow
    [SerializeField] private bool breakable;
    // Can be pulled towards the player by a grapple arrow (as opposed to the player being pulled towards it)
    [SerializeField] private bool pullable;
    // Can be ignighted by a flame arrow or another adjacent flame
    [SerializeField] private bool flamable;
    // Can be frozen by a freeze arrow
    [SerializeField] private bool freezable;

    // Breakable Specific Fields //
    [Header("Breakable Fields")]
    // Broken block to replace platform
    [SerializeField] private GameObject fracturedBlock;
    // Scale factor to ensure it scales to the correct size as the original object - defaulted to 0.5 to match a default cube
    [SerializeField] private float scaleFactor = 0.5f;

    // Pullable Specific Fields //
    [Header("Pullable Fields")]
    // How fast it gets pulled toward the player
    [SerializeField] private int pullSpeed = 4;
    // Checks if it is being pulled
    private bool isBeingPulled = false;
    private int groundLayer = 9;

    // Flamable Specific Fields //
    [Header("Flamable Fields")]
    // Should the particle effect be prewarmed (start at 100%) or start from 0 and build up to full
    [SerializeField] private bool prewarm;
    // What type of fire should eminate from the object
    [SerializeField] private GameObject fireType;
    // Scale factor to ensure the fire isn't a different size than the object
    [SerializeField] private float fireScaleFactor = 1f;
    // Where should the fire's center be
    [SerializeField] private Vector3 fireCenter;
    // Should the fire die out? If so, how quickly? -1 for no, any positive int for how fast it should die out
    [SerializeField] private float burnoutTime;
    // Used to spread fire
    private bool isOnFire = false;
    // Used to detect surrounding (flamable) objects
    Collider[] adjacentObjects = null;
    // How far away does an object need to be to catch fire from an already ignited object
    private float fireJumpDist = 0.75f;
    // How long it takes to spread the fire
    [SerializeField] private float fireSpread = 500;

    // Freezable Specific Fields //
    [Header ("Freezable Fields")]
    // How long until it takes to thaw
    [SerializeField] private float thawTime;
    // Ice material
    [SerializeField] private Material iceMat;
    // Normal material of object
    [SerializeField] private Rigidbody rigidBody;

    // If the object is frozen or not
    private bool isFrozen = false;
    private bool foreverBurn = false;
    private Renderer matRenderer;
    private Material normalMat;
    private float thawCountdown;
    private GameObject fire;

    private void Awake()
    {
        matRenderer = GetComponent<Renderer>();
        normalMat = matRenderer.material;
        isFrozen = false;
        if (burnoutTime <= 0) foreverBurn = true;
    }

    /**
     * Breaks block into smaller blocks
     */
    public void breakSelf()
    {
        if (breakable)
        {
            GameObject broken = Instantiate(fracturedBlock, transform.position, transform.rotation);
            broken.transform.localScale = new Vector3(this.transform.localScale.x * scaleFactor, this.transform.localScale.y * scaleFactor, this.transform.localScale.z * scaleFactor);
            Destroy(this.gameObject);
        }
    }

    /**
     * Pulls object towards player
     * @param dest Vector3 - the destination to get pulled towards
     */
    public bool getPulled()
    {
        return pullable;
    }

    public void setIsBeingPulled(bool pulledStatus)
    {
        isBeingPulled = pulledStatus;
    }

    public bool getIsBeingPulled()
    {
        if (isBeingPulled) return true;
        return false;
    }

    /*
     * Catches fire
     */
    public bool catchFire()
    {
        if (flamable && !isOnFire)
        {
            isOnFire = true;
            // Create the fire
            Vector3 fc = transform.position;
            if (fireCenter != Vector3.zero)
            {
                fc = new Vector3(fc.x + fireCenter.x, fc.y + fireCenter.y, fc.z + fireCenter.z);
            }
            fire = Instantiate(fireType, fc, transform.rotation);
            // Scale lenght and width but not height
            fire.transform.localScale = new Vector3(this.transform.localScale.x * fireScaleFactor, fire.transform.localScale.y, this.transform.localScale.z * fireScaleFactor);

            ParticleSystem firePS = fire.GetComponent<ParticleSystem>();

            if (!prewarm)
            {
                firePS.Clear();
            }
            return true;
        } else {
            return false;
        }
    }

    /*
     * Freezes object in place for a certain amount of time
     */
    public bool freeze()
    {
        if (freezable)
        {
            isFrozen = true;
            // Change material to ice
            matRenderer.material = iceMat;
            thawCountdown = thawTime;
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }
        return freezable;
    }

    private void Unfreeze() {
        isFrozen = false;
        matRenderer.material = normalMat;
        rigidBody.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isBeingPulled && collision.gameObject.layer != groundLayer && !collision.gameObject.CompareTag("Arrow"))
        {
            PlayerController.getInstance().StopPullingObject();
        }
    }

    // To visualize the range of fire spreading
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, fireJumpDist);
    }

    // Used to spread fire
    private void Update()
    {
        if (isFrozen) {
            thawCountdown -= Time.deltaTime;
            if (thawCountdown <= 0) {
                Unfreeze();
            }
        }

        if (flamable && isOnFire)
        {
            if (burnoutTime > 0) {
                burnoutTime -= Time.deltaTime;
            } else if (!foreverBurn) {
                // When its time to destroy, quickly fade out then destroy object
                Color tempColor = matRenderer.material.color;
                tempColor.a -= Time.deltaTime;
                if (tempColor.a >= 0)
                {
                    matRenderer.material.color = tempColor;
                }
                else
                {
                    Destroy(this.gameObject);
                    if (fire != null) Destroy(fire);
                }
            }

            // Check to see if adjacent objects are on fire
            if(adjacentObjects == null)
            {
                adjacentObjects = Physics.OverlapSphere(transform.position, fireJumpDist);
                Debug.Log("Adj objects " + adjacentObjects.Length);
            } else if (fireSpread >= 0){
                if (fireSpread == 0)
                {
                    for (int i = 0; i < adjacentObjects.Length; i++)
                    {
                        if (adjacentObjects[i].gameObject.tag == "Interactable")
                        {
                            Debug.Log("SPREADING");
                            adjacentObjects[i].SendMessage("catchFire");
                        }
                    }
                }
                fireSpread--;
            }
        }
    }

}
