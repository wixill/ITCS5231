using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFirst : MonoBehaviour
{
    //variable needed for animator
    static Animator anim;
    [SerializeField] private Transform trans;
    [SerializeField] private Transform Playertrans;
   

  
    private float turnSpeed;
 
    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;


    private float maxSpeed;
    private float radiusOfSat;



    void Start()
    {
        radiusOfSat = 1f;
        turnSpeed = 3f;
        maxSpeed = 1.5f;

        StartCoroutine(Disappear());
    }

    // Start is called before the first frame update
    void Update()
    {
        anim = GetComponent<Animator>();
       // LookatTarget();

    }

    



    // the function to set the target position
    void LookatTarget()
    {
        //sets the direction of the position
        seeTarget = new Vector3(Playertrans.position.x - trans.position.x, trans.position.y, Playertrans.position.z - trans.position.z);
        pRotate = Quaternion.LookRotation(seeTarget);
        //rotates the enemy to the player's position
        trans.rotation = Quaternion.Lerp(trans.rotation, pRotate, turnSpeed * Time.deltaTime);

    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }


}
