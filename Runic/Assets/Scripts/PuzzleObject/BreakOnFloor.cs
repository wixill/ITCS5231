using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnFloor : MonoBehaviour
{
    private AudioSource audioS;
    private Renderer rend;
    private BoxCollider bc;
    [SerializeField] private Rigidbody[] rigidBodies;
    private void Awake()
    {
        rend = gameObject.GetComponent<MeshRenderer>();
        audioS = gameObject.GetComponent<AudioSource>();
        bc = gameObject.GetComponent<BoxCollider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //rend.enabled = false;
            //if (bc != null) bc.enabled = false;
            for (int i = 0; i < rigidBodies.Length; i++)
            {
                if (rigidBodies[i] != null)
                {
                    rigidBodies[i].constraints = RigidbodyConstraints.FreezeAll;
                    rigidBodies[i].isKinematic = true;
                }
            }
            audioS.PlayOneShot(audioS.clip);

            Destroy(gameObject, 1.5f);
        }
    }
}
