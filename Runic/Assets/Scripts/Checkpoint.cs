using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private CheckpointManager cm;

    private void Start()
    {
        cm = GameObject.FindGameObjectWithTag("checkpointManager").GetComponent<CheckpointManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("something");
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 curPos = other.gameObject.transform.position;
            cm.checkpointPos = curPos;
            print("CHECKPOINT UPDATED TO: " + curPos);
        }
    }
}
