using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{

    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    private bool hitSomething;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hitSomething = false;
        trans.rotation = Quaternion.LookRotation(rb.velocity);

    }

    // Update is called once per frame
    void Update()
    {
        if (!hitSomething)
        {
            trans.rotation = Quaternion.LookRotation(rb.velocity);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
    }
}
