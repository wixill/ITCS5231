using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Child of Activator
// Activates once player is nearby and presses E to interact
// Then the button will turn on/off and activates the coresponding PuzzleObject
public class Button : Activator
{
    // Checks if it has been activated before or not
    private bool hasActivated = false;
    // Start is called before the first frame update
    void Start()
    {
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
            if (Input.GetKeyDown("E"))
            {
                activate();
                hasActivated = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("player left button radius");
    }

}
