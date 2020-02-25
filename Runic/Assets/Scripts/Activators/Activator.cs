using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class Activator
// Has a list of PuzzleObjects that it then activates
public class Activator : MonoBehaviour
{
    // List of puzzle objects to activate
    [SerializeField] protected List<PuzzleObject> toActivate;
    // Checks if it starts on or off (true for on, false for off)
    [SerializeField] protected bool startOn;
    // Checks if it is toggable or not 
    [SerializeField] protected bool isToggleable;

    // Activates each puzzle object in the toActivate list
    public void activate()
    {
        foreach(PuzzleObject x in toActivate)
        {
            x.activate();
        }
    }

}
