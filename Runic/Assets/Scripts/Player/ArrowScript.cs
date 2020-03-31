using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    private float lifeTimer = 10f;
    private float timer = 0;
    private bool hitSomething = false;
    private Vector3 previousVel;


    private void Start()
    {
        trans.rotation = Quaternion.LookRotation(rb.velocity);
        previousVel = rb.velocity;
        //transform.Rotate(72.0f, trans.rotation.y, trans.rotation.z, Space.Self);
        //print(trans.position);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //if (timer >= lifeTimer) Destroy(gameObject);
        
        if (!hitSomething)
        {
            print(rb.velocity);
            trans.rotation = Quaternion.LookRotation(rb.velocity);
            //print(trans.rotation);
            //print("rotate");
            //transform.Rotate(72.0f, trans.rotation.y, trans.rotation.z, Space.Self);
        }
        previousVel = rb.velocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitSomething) return;
        hitSomething = true;
        //print("freeze");
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
