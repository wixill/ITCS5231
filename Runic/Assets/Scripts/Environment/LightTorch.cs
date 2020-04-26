using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : Activator
{
    // Does it activate TorchKeys? Should be an Anti_Torch
    [SerializeField] bool isActivator = false;
    // Checks if its been lit or not to prevent toggling
    private bool isLit = false;
    // Checks if it has been activated before or not
    private bool hasActivated = false;
   

    /*
     * Ignights the torch by activating its children
     * Its children are two particle systems, Flame and Light
     * If its an Anti_Torch activator then it also activates its resective Torch_Key
     */
    public bool lightTorch()
    {
        if (!isLit)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).gameObject.activeSelf)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                }
            }

            if (isActivator)
            {
                activate();
            }

        }
        isLit = true;
        return true;

    }
}
