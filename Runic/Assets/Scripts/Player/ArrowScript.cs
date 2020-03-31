using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    private float lifeTimer = 6f;
    private float timer = 0;
    private bool hitSomething = false;

    private void Start()
    {
        trans.rotation = Quaternion.LookRotation(rb.velocity);
    }


    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTimer) Destroy(gameObject);
        
        if (!hitSomething)
        {
            print(rb.velocity);
            trans.rotation = Quaternion.LookRotation(rb.velocity);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hitSomething) return;
        hitSomething = true;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
