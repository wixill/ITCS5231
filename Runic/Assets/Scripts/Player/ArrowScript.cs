using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform grapplePoint;
    private PlayerController shooterController;
    private float lifeTimer = 6f;
    private float timer = 0;
    private bool hitSomething = false;
    private bool stuck = false;
    private Vector3 stickPosition;
    private Quaternion stickRotation;
    private ArrowType type;

    private void Awake()
    {
        shooterController = PlayerController.getInstance();
    }

    private void Start()
    {
        trans.rotation = Quaternion.LookRotation(rb.velocity);
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer) Destroy(gameObject);
        
        if (!hitSomething)
        {
            stickPosition = trans.position;
            stickRotation = trans.rotation;
            trans.rotation = Quaternion.LookRotation(rb.velocity);
        } else if (!stuck) {
            trans.rotation = stickRotation;
            trans.position = stickPosition;
            stuck = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (hitSomething) return;
        print("This arrow is: " + type);
        hitSomething = true;
        rb.useGravity = false;
        rb.velocity = new Vector3(0,0,0);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        if (type == ArrowType.Grapple) {
            if (collision.gameObject.tag == "Interactable") {
                ArrowInteraction pullTarget = collision.gameObject.GetComponent<ArrowInteraction>();
                if (pullTarget.getPulled()) {
                    shooterController.StartGrappleFrom(collision.gameObject);
                } else {
                    shooterController.StartGrappleTo(grapplePoint.position);
                }
            } else if (collision.gameObject.layer != 9) {
                shooterController.StartGrappleTo(grapplePoint.position);
            }
        }
    }

    public void setType(ArrowType type) {
        this.type = type;
    }
}
