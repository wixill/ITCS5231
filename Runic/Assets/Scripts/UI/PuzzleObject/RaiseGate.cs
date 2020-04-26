using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseGate : PuzzleObject
{
    // Aimator for the gate
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Raises the gate by setting the trigger in the animator to true
    public override void activate()
    {
        anim.SetTrigger("RaiseGate");
    }
}



