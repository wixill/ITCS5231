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

    [SerializeField] public Transform[] waypoints;
    private float timeBetweenShots;
    public float startTimeBetweenShots;

    
    private float numOfHits;
  
 

    bool moving = false;
    bool ready = false;
    bool loading = false;
    bool shooting = false;
    bool gothit = false;

    int showArrow = 0;


    int curW = 0;
    public float speed = 2f;

  


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    
        timeBetweenShots = startTimeBetweenShots;

    }

    // Update is called once per frame
    void Update()
    {
        
        //if the player is now close to the enemy

       
            trans.LookAt(player.position);

            if (timeBetweenShots <= 0)
            {

                Shoot();
                timeBetweenShots = startTimeBetweenShots;

            }
            else
            {

                timeBetweenShots -= Time.deltaTime;



            }
            if (trans.position != waypoints[curW].position)
            {
                trans.position = Vector3.MoveTowards(trans.position, waypoints[curW].position, speed * Time.deltaTime);

            }

            else
            {
                curW = (curW + 1) % waypoints.Length;

            }
        



    }


    void Shoot()
    {

        Vector3 shootingV = new Vector3(trans.position.x - .35f, trans.position.y + 1.1f, trans.position.z + 0.2f);
        GameObject a = Instantiate(arrowPrefab) as GameObject;

        a.transform.position = shootingV;
        Rigidbody b = a.GetComponent<Rigidbody>();
        b.velocity = trans.forward * 30f;
    }


    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            numOfHits++;
            if (numOfHits == 2)
            {
                StartCoroutine(Disappear());

                //sets the boolean in Animator. since moving is true, this will make the enemy move
                anim.SetBool("gotHit", false);
                boots.SetBool("gotHit", false);
                headgear.SetBool("gotHit", false);
                belt.SetBool("gotHit", false);
                cape.SetBool("gotHit", false);
                chestplates.SetBool("gotHit", false);
                gloves.SetBool("gotHit", false);
                handpads.SetBool("gotHit", false);
                kneepads.SetBool("gotHit", false);
                quiver.SetBool("gotHit", false);
                shoulder.SetBool("gotHit", false);
                skirt.SetBool("gotHit", false);
                tabard.SetBool("gotHit", false);
                bow.SetBool("gotHit", false);
            }

        }
    }


}

