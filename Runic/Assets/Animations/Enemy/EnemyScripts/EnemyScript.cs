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
    [SerializeField] private float shootingForce;
    [SerializeField] private Animator boots;
    [SerializeField] private Animator headgear;
    [SerializeField] private Animator belt;
    [SerializeField] private Animator cape;
    [SerializeField] private Animator chestplates;
    [SerializeField] private Animator gloves;
    [SerializeField] private Animator handpads;
    [SerializeField] private Animator kneepads;
    [SerializeField] private Animator quiver;
    [SerializeField] private Animator shoulder;
    [SerializeField] private Animator skirt;
    [SerializeField] private Animator tabard;
    [SerializeField] private Animator bow;
    public GameObject arrowPrefab;

    private float timeBetweenShots;
    public float startTimeBetweenShots;

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
    bool ready = false;
    bool loading = false;
    bool shooting = false;
    bool wait = false;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        radiusOfSat = 1f;
        turnSpeed = 3f;
        maxSpeed = 1.5f;
        timeBetweenShots = startTimeBetweenShots;

    }

    // Update is called once per frame
    void Update()
    {
        //goes to the target location

        GoToTargetPos();


        if (trans.position == targetPosition)
        {
            moving = false;
            rb.velocity = Vector3.zero;
            anim.SetBool("isMoving", moving);
            boots.SetBool("isMoving", moving);
            headgear.SetBool("isMoving", moving);
            belt.SetBool("isMoving", moving);
            cape.SetBool("isMoving", moving);
            chestplates.SetBool("isMoving", moving);
            gloves.SetBool("isMoving", moving);
            handpads.SetBool("isMoving", moving);
            kneepads.SetBool("isMoving", moving);
            quiver.SetBool("isMoving", moving);
            shoulder.SetBool("isMoving", moving);
            skirt.SetBool("isMoving", moving);
            tabard.SetBool("isMoving", moving);
            bow.SetBool("isMoving", moving);
        }

        if (moving)
        {
            MovePlayer();
        }

        else
        {
            
            if (timeBetweenShots <= 0)
            {
                
                Shoot();
                timeBetweenShots = startTimeBetweenShots;
            }
            else
            {
                timeBetweenShots -= Time.deltaTime;
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
        boots.SetBool("isMoving", moving);
        headgear.SetBool("isMoving", moving);
        belt.SetBool("isMoving", moving);
        cape.SetBool("isMoving", moving);
        chestplates.SetBool("isMoving", moving);
        gloves.SetBool("isMoving", moving);
        handpads.SetBool("isMoving", moving);
        kneepads.SetBool("isMoving", moving);
        quiver.SetBool("isMoving", moving);
        shoulder.SetBool("isMoving", moving);
        skirt.SetBool("isMoving", moving);
        tabard.SetBool("isMoving", moving);
        bow.SetBool("isMoving", moving);
    }
    
    void Shoot()
    {
        Vector3 shootingV = new Vector3(trans.position.x+0.7f, 1f, trans.position.z - 0.5f);
        GameObject a = Instantiate(arrowPrefab) as GameObject;
        a.transform.position = shootingV;
        Rigidbody b = a.GetComponent<Rigidbody>();
        //a.transform.Rotate(.5f, 0.0f, 0.0f, Space.Self);
        b.velocity = trans.forward * 20f;
        wait = false;

    }
 
 
}

