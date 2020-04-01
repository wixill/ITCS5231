using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerParent : MonoBehaviour
{
    [SerializeField] private GameObject parent;

    private void OnTriggerEnter(Collider other)
    {
        parent.SendMessage("OnTriggerEnter", other);
    }
}
