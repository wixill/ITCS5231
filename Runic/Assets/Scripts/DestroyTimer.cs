using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Destroys object after a certain amount of time
// Object qickly fades out before destroying itself
// Object's shader must be set to Fade or Transparent in order for the fade to work
public class DestroyTimer : MonoBehaviour
{

    // How long until it destroys itself after creation
    [SerializeField] private float destroyCountdown;
    // Holds drop down options for what will cause the destroy countdown to start
    [HideInInspector] public string[] startTrigger = new string[]{"Start", "Collision", "Trigger"};
    [HideInInspector] public int startTriggerIndex = 0;

    private bool countdownStart = false;
    
    // Used to change the object's transparency
    private Color alphaColor;

    // Start is called before the first frame update
    void Start()
    {
        if(startTriggerIndex == 0)
        {
            countdownStart = true;
        }
        alphaColor = GetComponent<MeshRenderer>().material.color;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(startTriggerIndex == 1)
        {
            countdownStart = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(startTriggerIndex == 2)
        {
            countdownStart = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy after a certain amount of time
        if (countdownStart)
        {
            destroyCountdown -= Time.deltaTime;
            if (destroyCountdown <= 0)
            {
                // When its time to destroy, quickly fade out then destroy object
                Color newColor = alphaColor;
                newColor.a -= Time.deltaTime;
                if (newColor.a >= 0)
                {
                    alphaColor = newColor;
                    GetComponent<MeshRenderer>().material.color = alphaColor;
                } else
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
