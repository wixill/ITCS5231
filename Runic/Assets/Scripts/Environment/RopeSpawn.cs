using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject partPrefab, parentObject;

    [SerializeField]
    [Range(1, 1000)]
    private int length = 1;

    [SerializeField]
    private float partDistance = 0.21f;

    [SerializeField]
    private bool reset, spawn, snapFirst, snapLast;


    // Update is called once per frame
    void Update()
    {
        if (reset)
        {
            foreach (GameObject x in GameObject.FindGameObjectsWithTag("Rope"))
            {
                Destroy(x);
            }

            reset = false;
        }
        if (spawn)
        {
            Spawn();
            spawn = false;
        }
    }

    public void Spawn()
    {
        // Keeps track of how long the rope will be/how many pieces we need to construct it
        int count = (int)(length / partDistance);

        for(int x = 0; x < count; x++)
        {
            GameObject temp;
            temp = Instantiate(partPrefab, new Vector3(transform.position.x, transform.position.y + partDistance * (x + 1), transform.position.z), Quaternion.identity, parentObject.transform);
            temp.transform.eulerAngles = new Vector3(180, 0, 0);

            temp.name = parentObject.transform.childCount.ToString();

            if(x == 0)
            {
                Destroy(temp.GetComponent<CharacterJoint>());
                if (snapFirst)
                {
                    temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
            } else
            {
                temp.GetComponent<CharacterJoint>().connectedBody = parentObject.transform.Find((parentObject.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }

        if (snapLast)
        {
            parentObject.transform.Find((parentObject.transform.childCount).ToString()).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
