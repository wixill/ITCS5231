using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    //variable needed for animator
    static Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform player;
    //[SerializeField] private float shootingForce;
    //[SerializeField] private Animator boots;
    //[SerializeField] private Animator headgear;
    //[SerializeField] private Animator belt;
    //[SerializeField] private Animator cape;
    //[SerializeField] private Animator chestplates;
    //[SerializeField] private Animator gloves;
    //[SerializeField] private Animator handpads;
    //[SerializeField] private Animator kneepads;
    //[SerializeField] private Animator quiver;
    //[SerializeField] private Animator shoulder;
    //[SerializeField] private Animator skirt;
    //[SerializeField] private Animator tabard;
    //[SerializeField] private Animator bow;
    public GameObject arrowPrefab;
    public GameObject playerArrow;
 
    
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    
    private float maxSpeed;
    private float radiusOfSat;
    private float turnSpeed;
  
 

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
        
        //if the player is now close to the enemy

        if(Vector3.Distance(player.position, trans.position) <= 30)
        {
            trans.LookAt(player.position);
            anim.SetBool("isSeen", true);
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
    

        }
    }


}

