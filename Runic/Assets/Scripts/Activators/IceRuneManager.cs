using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRuneManager : Activator
{
    // Parent RuneClock object
    [SerializeField] GameObject runeClock;
    // Number of Torch_Key objects left unlit
    private int runesLeft = 3;
    // Checks if it has been activated before or not
    private bool hasActivated = false;
    // RuneClock reference to turn off the counter
    private RuneClock rc;

    private void Start()
    {
        rc = runeClock.GetComponent<RuneClock>();
    }

    private void Update()
    {
        if (runesLeft == 0 && !hasActivated)
        {
            hasActivated = true;
            rc.stopClock();
            activate();
        }
    }

    /*
     *  Decrements the torchesLeft by one
     */
    public void decrementRunes()
    {
        runesLeft--;
    }
}
