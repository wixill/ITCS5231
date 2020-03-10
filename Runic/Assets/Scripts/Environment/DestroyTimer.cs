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
    private bool countdownStart = false;

    // Used to change the object's transparency
    private Color alphaColor;

    // Start is called before the first frame update
    void Start()
    {
        countdownStart = true;
        alphaColor = GetComponent<MeshRenderer>().material.color;

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
