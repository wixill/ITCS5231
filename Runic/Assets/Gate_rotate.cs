using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate_rotate : MonoBehaviour
{
    private Vector3 pivotPoint;

    private void Start()
    {
        pivotPoint = transform.position;
        pivotPoint.y = 0;
        pivotPoint.z = 0;
    }
    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pivotPoint, Vector3.up, 30 * Time.deltaTime);
    }
}
