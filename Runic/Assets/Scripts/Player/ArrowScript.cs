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
    private int groundLayer = 9;

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
        if (collision.collider.tag != "Arrow") {
            print("This arrow is: " + type);
            hitSomething = true;
            rb.useGravity = false;
            rb.velocity = new Vector3(0,0,0);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            switch (type)
            {
                case ArrowType.Standard:
                    if(collision.gameObject.tag == "Interactable")
                    {
                        collision.gameObject.GetComponent<ArrowInteraction>().breakSelf();
                    }
                    break;

                case ArrowType.Grapple:
                    if (collision.gameObject.tag == "Interactable")
                    {
                        ArrowInteraction pullTarget = collision.gameObject.GetComponent<ArrowInteraction>();
                        if (pullTarget.getPulled())
                        {
                            shooterController.StartGrappleFrom(collision.gameObject);
                            pullTarget.setIsBeingPulled(true);
                        }
                        else
                        {
                            shooterController.StartGrappleTo(grapplePoint.position);
                        }
                    }
                    else if (collision.gameObject.layer != groundLayer)
                    {
                        shooterController.StartGrappleTo(grapplePoint.position);
                    }
                    break;

                case ArrowType.Flame:
                    if (collision.gameObject.tag == "Interactable")
                    {
                        collision.gameObject.GetComponent<ArrowInteraction>().catchFire();
                    }
                    break;

                case ArrowType.Freeze:
                    if (collision.gameObject.tag == "Interactable")
                    {
                        collision.gameObject.GetComponent<ArrowInteraction>().freeze();
                    }
                    break;
            }

        }
    }

    public void setType(ArrowType type) {
        this.type = type;
    }
}
