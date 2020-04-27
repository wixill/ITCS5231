using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private Transform grapplePoint;
    [SerializeField] private Color standardColor;
    [SerializeField] private Color grappleColor;
    [SerializeField] private Color flameColor;
    [SerializeField] private Color freezeColor;

    private PlayerController shooterController;
    private float lifeTimer = 3f;
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
        if (collision.collider.tag != "Arrow" && collision.collider.tag != "Player") {
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
                        if (collision.gameObject.GetComponent<ArrowInteraction>().catchFire()) {
                            shooterController.StartFlameCooldown();
                        }
                    } else if (collision.gameObject.tag == "LightableTorch")
                    {
                        if (collision.gameObject.GetComponent<LightTorch>().lightTorch())
                        {
                            shooterController.StartFlameCooldown();
                        }
                    }
                    break;

                case ArrowType.Freeze:
                    if (collision.gameObject.tag == "Interactable")
                    {
                        if (collision.gameObject.GetComponent<ArrowInteraction>().freeze()) {
                            shooterController.StartFreezeCooldown();
                        }
                    } else if (collision.gameObject.tag == "IceRune")
                    {
                        if (collision.gameObject.GetComponent<IceRune_activator>().freezeSelf())
                        {
                            shooterController.StartFreezeCooldown();
                        }
                    }
                    break;
            }

        }
    }

    public void setType(ArrowType type) {
        this.type = type;
        switch (type)
        {
            case ArrowType.Standard:
                trail.material.color = standardColor;
                break;
            case ArrowType.Grapple:
                trail.material.color = grappleColor;
                break;
            case ArrowType.Flame:
                trail.material.color = flameColor;
                break;
            case ArrowType.Freeze:
                trail.material.color = freezeColor;
                break;
        }
    }

    public ArrowType getType()
    {
        return this.type;
    }
}
