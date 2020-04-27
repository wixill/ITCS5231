using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seesaw : MonoBehaviour
{
    [SerializeField] private float playerForce = 1f;
    [SerializeField] private float objectForce = 4f;

    private Rigidbody rb;
    private bool hasWeight;
    private GameObject weight = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        hasWeight = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasWeight && weight == null) {
            rb.isKinematic = false;
            hasWeight = false;
            if (rb.rotation.x != 0) {
                Quaternion newRot = Quaternion.Lerp(rb.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime);
                rb.MoveRotation(newRot);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasWeight || other.tag == "Arrow") return;
        if (other.tag == "Player") {
            Vector3 point = other.transform.position - transform.position;
            Vector3 force = new Vector3(0, -1 * playerForce, 0);
            rb.AddForceAtPosition(force, other.transform.position, ForceMode.Force);
        } else {
            Vector3 point = other.transform.position - transform.position;
            Vector3 force = new Vector3(0, -1 * objectForce, 0);
            rb.AddForceAtPosition(force, other.transform.position, ForceMode.Impulse);
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (hasWeight || other.tag == "Arrow") return;
        if (other.tag == "Player")
        {
            Vector3 point = other.transform.position - transform.position;
            Vector3 force = new Vector3(0, -1 * playerForce, 0);
            rb.AddForceAtPosition(force, other.transform.position, ForceMode.Force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag != "Arrow" && collision.gameObject.layer != 9) {
            StartCoroutine(WaitForRotate(collision.gameObject));
        }
    }

    IEnumerator WaitForRotate(GameObject other) {
        yield return new WaitForSeconds(0.1f);
        if (!hasWeight) {
            Rigidbody otherRB;
            try {
                otherRB = other.gameObject.GetComponent<Rigidbody>();
            } catch (MissingComponentException e) {
                yield break;
            }
            otherRB.constraints = RigidbodyConstraints.FreezePosition;
            otherRB.constraints = RigidbodyConstraints.FreezeRotationY;
            rb.isKinematic = true;
            weight = other;
            hasWeight = true;
        }
    }
}