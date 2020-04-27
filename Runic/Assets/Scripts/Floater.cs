using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{

    [SerializeField] private float speed = 0.02f;
    [SerializeField] private float height = 0.05f;
    [SerializeField] private float rotationSpeed = 0.0f;
    private float startY;
    private bool canChange;


    private void Start()
    {
        startY = transform.position.y;
        canChange = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = transform.position;
        float dif = Mathf.Abs(currentPos.y - startY);
        if (dif >= height && canChange) {
            canChange = false;
            speed *= -1;
            StartCoroutine(WaitTimeForFloat());
        }
        float newY = currentPos.y + speed * Time.deltaTime;
        transform.position = new Vector3(currentPos.x, newY, currentPos.z);

        if (rotationSpeed != 0) {
            transform.Rotate(0.0f, rotationSpeed * Time.deltaTime, 0.0f, Space.World);
        }
        
    }

    IEnumerator WaitTimeForFloat()
    {
        yield return new WaitForSeconds(3f);
        canChange = true;
    }
}
