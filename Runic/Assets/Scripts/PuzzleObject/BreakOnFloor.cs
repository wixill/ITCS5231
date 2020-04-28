using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnFloor : MonoBehaviour
{
    private AudioSource audioS;
    private Renderer rend;
    private BoxCollider bc;

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
            rend.enabled = false;
            if (bc != null) bc.enabled = false;
            audioS.PlayOneShot(audioS.clip, 1.0f);

            Destroy(gameObject, 3.0f);
        }
    }
}
