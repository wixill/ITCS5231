using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of Activator
// Activates once player is nearby and presses E to interact
// Then the button will turn on/off and activates the coresponding PuzzleObject
public class Button : Activator
{
    // Color when button is off
    [SerializeField] private Color offColor;
    // Color when button is on
    [SerializeField] private Color onColor;

    // Renderer
    private Renderer[] rend;

    private AudioSource audioS;

    // Checks if it has been activated before or not
    private bool hasActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].material.SetColor("_EmissionColor", offColor);
        }

        if (startOn)
        {
            activate();
            hasActivated = true;
        }
        audioS = gameObject.GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Arrow"){
            if (!hasActivated || isToggleable)
            {
                ArrowType aType = collision.gameObject.GetComponent<ArrowScript>().getType();
                if (aType == ArrowType.Standard) {
                    activate();
                    hasActivated = true;
                    if (rend[0].material.GetColor("_EmissionColor") == offColor)
                    {
                        for (int i = 0; i < rend.Length; i++)
                        {
                            rend[i].material.SetColor("_EmissionColor", onColor);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < rend.Length; i++)
                        {
                            rend[i].material.SetColor("_EmissionColor", offColor);
                        }
                    }
                    audioS.PlayOneShot(audioS.clip, 1.0f);
                }
            }
        }

    }
}
