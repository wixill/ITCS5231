using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //variable needed for animator
    static Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float distanceOffset;
    [SerializeField] private float angleOffset;


    private float maxSpeed;
    private float radiusOfSat;
    private float turnSpeed;
    //Vector that will hold the target position
    Vector3 targetPosition;
    Vector3 targetPoint;
    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;

    bool moving = false;
 


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        radiusOfSat = 1f;
        turnSpeed = 3f;
        maxSpeed = 1.5f;

    }

    // Update is called once per frame
    void Update()
    {
        //goes to the target location
        GoToTargetPos();

        if (trans.position == targetPosition)
        {
            rb.velocity = Vector3.zero;
            moving = false;
            anim.SetBool("isMoving", moving);
        }

        else
        {

            //will move the character if moving is true
            if (moving)
            {
                MovePlayer();

            }

            else
            {
                Shoot();
            }
        }
    }

    // the function to set the target position
    void GoToTargetPos()
    {
        //sets the direction of the position
        seeTarget = new Vector3(Playertrans.position.x - trans.position.x, trans.position.y, Playertrans.position.z - trans.position.z);

        //Gets the point forward from the leader's foward facing vector
        targetPoint = Playertrans.forward * distanceOffset;

        //sets the position in the formation
        targetPosition = Quaternion.Euler(0f, angleOffset, 0f) * targetPoint;

        //updates enemy position in the formation
        targetPosition += Playertrans.position;
        targetPosition = new Vector3(targetPosition.x, 0f, targetPosition.z);
        //allows the character to rotate to the target position
        pRotate = Quaternion.LookRotation(seeTarget);
        moving = true;     
    }

    void MovePlayer()
    {
        //rotates the enemy to the player's position
        trans.rotation = Quaternion.Lerp(trans.rotation, pRotate, turnSpeed * Time.deltaTime);

        trans.position = Vector3.MoveTowards(trans.position, targetPosition, maxSpeed * Time.deltaTime);

        //sets the speed in Animator 
        //anim.SetFloat("Speed", maxSpeed * Time.deltaTime);

        //sets the boolean in Animator. since moving is true, this will make the enemy move
        anim.SetBool("isMoving", moving);
    }

    void Shoot()
    {
        //shooting code will go here
    }
}

