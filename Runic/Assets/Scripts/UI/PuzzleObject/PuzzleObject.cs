using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class outlining a PuzzleObject
// A PuzzleObject is an object that can be activated by an Activator object
public abstract class PuzzleObject : MonoBehaviour
{
    //Activates the puzzle object
    public abstract void activate();
    
}
