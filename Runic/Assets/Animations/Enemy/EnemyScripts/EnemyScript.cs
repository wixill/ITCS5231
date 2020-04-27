using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    //variable needed for animator
    static Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform Playertrans;
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
    public GameObject playerArrow;
    public NavMeshAgent agent;
    
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    private float maxSpeed;
    private float radiusOfSat;
    private float turnSpeed;
    int hiddenArrows = 1;
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
    bool gothit = false;

    int showArrow = 0;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        radiusOfSat = 1f;
        turnSpeed = 3f;
        maxSpeed = 1.5f;
        timeBetweenShots = startTimeBetweenShots;

    }

    // Update is called once per frame
    void Update()
    {
        //goes to the target location

                //MovePlayer();
                agent.SetDestination(Playertrans.position);
                timeBetweenShots = startTimeBetweenShots;
       
      

        /*
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
        */
      
    }

 
    

    // the function to set the target position
    void GoToTargetPos()
    {
        //sets the direction of the position
        seeTarget = new Vector3(Playertrans.position.x - trans.position.x, trans.position.y, Playertrans.position.z - trans.position.z);

        //allows the character to rotate to the target position
        pRotate = Quaternion.LookRotation(seeTarget);

        //rotates the character to the target position
        trans.rotation = Quaternion.Lerp(trans.rotation, pRotate, turnSpeed * Time.deltaTime);
        moving = true;
    }

    void MovePlayer()
    {
        targetPosition += Playertrans.position;
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
        agent.SetDestination(Playertrans.position);
    }
    
    void Shoot()
    {
     
       

            Vector3 shootingV = new Vector3(trans.position.x + 0.55f, 1.1f, trans.position.z - 0.5f);
            GameObject a = Instantiate(arrowPrefab) as GameObject;

            a.transform.position = shootingV;
            Rigidbody b = a.GetComponent<Rigidbody>();
            b.velocity = trans.forward * 20f;
           
     
        
    

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            gothit = true;
            //sets the boolean in Animator. since moving is true, this will make the enemy move
            anim.SetBool("gotHit", gothit);
            boots.SetBool("gotHit", gothit);
            headgear.SetBool("gotHit", gothit);
            belt.SetBool("gotHit", gothit);
            cape.SetBool("gotHit", gothit);
            chestplates.SetBool("gotHit", gothit);
            gloves.SetBool("gotHit", gothit);
            handpads.SetBool("gotHit", gothit);
            kneepads.SetBool("gotHit", gothit);
            quiver.SetBool("gotHit", gothit);
            shoulder.SetBool("gotHit", gothit);
            skirt.SetBool("gotHit", gothit);
            tabard.SetBool("gotHit", gothit);
            bow.SetBool("gotHit", gothit);

        }
    }


}

