using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchKeyManager : Activator
{
    // Number of Torch_Key objects left unlit
    private int torchesLeft = 4;
    // Checks if it has been activated before or not
    private bool hasActivated = false;

    private void Update()
    {
        if (torchesLeft == 0 && !hasActivated)
        {
            hasActivated = true;
            activate();
        }
    }

    /*
     *  Decrements the torchesLeft by one
     */
    public void decrementTorches()
    {
        torchesLeft--;
    }

}
