using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirst : MonoBehaviour
{
    //variable needed for animator
   [SerializeField]Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform Playertrans;

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
    private float timeBetweenShots;
    public float startTimeBetweenShots;
    public GameObject arrowPrefab;




    private float turnSpeed;
 
    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;


    private float maxSpeed;
    private float radiusOfSat;
    private bool gotHit = false;
    private float numOfHits = 0;

    void Start()
    {
        radiusOfSat = 1f;
        turnSpeed = 1f;
        maxSpeed = 1.5f;
        timeBetweenShots = startTimeBetweenShots;
        
        //anim.SetBool("isReady", true);


       // StartCoroutine(Disappear());
    }

    // Start is called before the first frame update
    void Update()
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

   
    // the function to set the target position

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow")
        {
            Destroy(collision.gameObject);
            numOfHits++;
           if(numOfHits == 2) {
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

    void Shoot()
    {

        Vector3 shootingV = new Vector3(trans.position.x - .35f, trans.position.y + 1.1f, trans.position.z + 0.2f);
        GameObject a = Instantiate(arrowPrefab) as GameObject;

        a.transform.position = shootingV;
        Rigidbody b = a.GetComponent<Rigidbody>();
        b.velocity = trans.forward * 30f;
    }

}
