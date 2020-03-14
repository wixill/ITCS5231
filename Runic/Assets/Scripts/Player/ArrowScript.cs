using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private Transform trans;
    [SerializeField] private Rigidbody rb;
    private bool hitSomething = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        trans.rotation = Quaternion.LookRotation(rb.velocity);
        transform.Rotate(72.0f, trans.rotation.y, trans.rotation.z, Space.Self);
    }

    private void Update()
    {
        if (!hitSomething)
        {
            trans.rotation = Quaternion.LookRotation(rb.velocity);
            transform.Rotate(72.0f, trans.rotation.y, trans.rotation.z, Space.Self);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        hitSomething = true;
    }
}
