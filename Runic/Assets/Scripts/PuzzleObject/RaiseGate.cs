using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseGate : PuzzleObject
{
    // Aimator for the gate
    private Animator anim;
    private AudioSource audioS;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float volume;
    [SerializeField] private AudioClip rockfall;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        audioS = gameObject.GetComponent<AudioSource>();
    }

    // Raises the gate by setting the trigger in the animator to true
    public override void activate()
    {
        anim.SetTrigger("RaiseGate");
        audioS.PlayOneShot(audioS.clip);
        if(rockfall != null)
        {
            audioS.PlayOneShot(rockfall, volume);
        }
    }
}



