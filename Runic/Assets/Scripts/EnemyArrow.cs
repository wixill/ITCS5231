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
        transform.Rotate(-20f, 0.0f, 0.0f, Space.World);

    }

    // Update is called once per frame
    void Update()
    {
        if (!hitSomething)
        {
            trans.rotation = Quaternion.LookRotation(rb.velocity);
            transform.Rotate(20f, 0.0f, 0.0f, Space.World);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
    }
}
