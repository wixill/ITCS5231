using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //variable needed for animator
    static Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform Playertrans;

    private float maxSpeed;
    private float radiusOfSat;
    private float turnSpeed;
    //Vector that will hold the target position
    Vector3 targetPosition;

    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;

    bool moving = false;
 


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        radiusOfSat = 1f;
        turnSpeed = 3f;
        maxSpeed = 3f;

    }

    // Update is called once per frame
    void Update()
    {
        //if the distance between the enemy and the player is greater than 5, we will move the enemy towards the player for now
        if (Vector3.Distance(trans.position, Playertrans.position) > 10f)
        {
            //goes to the target location
            GoToTargetPos();
        }
        
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

    // the function to set the target position
    void GoToTargetPos()
    {
        //sets the direction of the position
        seeTarget = new Vector3(Playertrans.position.x - trans.position.x, 1f, Playertrans.position.z - trans.position.z);
        
        //allows the character to rotate to the target position
        pRotate = Quaternion.LookRotation(seeTarget);
        moving = true;     
    }

    void MovePlayer()
    {
        //rotates the enemy to the player's position
        trans.rotation = Quaternion.Lerp(trans.rotation, pRotate, turnSpeed * Time.deltaTime);

        //moves the enemy to the target position
        trans.position = Vector3.MoveTowards(Playertrans.position, targetPosition, maxSpeed * Time.deltaTime);

        //sets the speed in Animator 
        anim.SetFloat("maxSpeed", maxSpeed * Time.deltaTime);

        //sets the boolean in Animator. since moving is true, this will make the enemy move
        anim.SetBool("isMoving", moving);

        //if we have reached the target destination
        if (Vector3.Distance(trans.position, Playertrans.position) < 10f)
        {
            //gets our character to stop moving and then sets the boolean in Animator to false
            moving = false;
            anim.SetBool("isWalking", moving);
        }

    }

    void Shoot()
    {
        //shooting code will go here
    }
}

