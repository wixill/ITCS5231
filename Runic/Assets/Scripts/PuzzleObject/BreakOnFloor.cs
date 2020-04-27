using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakOnFloor : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
    }
}
