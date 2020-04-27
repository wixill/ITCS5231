using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirst : MonoBehaviour
{
    //variable needed for animator
    static Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform Playertrans;
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

        //StartCoroutine(Disappear());
    }

    // Start is called before the first frame update
    void Update()
    {
        
        anim = GetComponent<Animator>();


       

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
            trans.position = trans.position;
           if(numOfHits == 2) {
                StartCoroutine(Disappear());
                gotHit = true;
                //sets the boolean in Animator. since moving is true, this will make the enemy move
                anim.SetBool("gotHit", gotHit);
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
