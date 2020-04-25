using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Activator
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
        for (int i = 0; i < rend.Length; i++) {
            rend[i].material.SetColor("_EmissionColor", offColor);
        }
        

        if (startOn)
        {
            activate();
            hasActivated = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasActivated || isToggleable)
        {
            if (other.GetComponent<Rigidbody>().mass > 1)
            {
                activate();
                hasActivated = true;
                for (int i = 0; i < rend.Length; i++)
                {
                    rend[i].material.SetColor("_EmissionColor", onColor);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (isToggleable)
        {
            rend[0].material.SetColor("_EmissionColor", offColor);
        }
    }
}
