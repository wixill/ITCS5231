using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Parent class Activator
// Has a list of PuzzleObjects that it then activates
public class Activator : MonoBehaviour
{
    // List of puzzle objects to activate
    [SerializeField] private List<PuzzleObject> toActivate;

    // Activates each puzzle object in the toActivate list
    public void activate()
    {
        foreach(PuzzleObject x in toActivate)
        {
            x.activate();
        }
    }

}
