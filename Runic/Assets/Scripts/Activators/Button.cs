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

    // Checks if it has been activated before or not
    private bool hasActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        rend[0].material.SetColor("_EmissionColor", offColor);

        if (startOn)
        {
            activate();
            hasActivated = true;
        }
    }

    // When player gets near the button, display "E" to interact if the button can be activated
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered collider box");

        if(!hasActivated || isToggleable)
        {
            Debug.Log("press E to interact");
        }
        
    }

    // If the player is near button and they press E, then activate button
    private void OnTriggerStay(Collider other)
    {
        if (!hasActivated || isToggleable)
        {
            if (Input.GetButtonDown("Interact"))
            {
                activate();
                hasActivated = true;
                if (rend[0].material.GetColor("_EmissionColor") == offColor)
                {
                    rend[0].material.SetColor("_EmissionColor", onColor);
                }
                else
                {
                    rend[0].material.SetColor("_EmissionColor", offColor);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("player left button radius");
    }

}
