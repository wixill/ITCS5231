using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveWhenActivated : PuzzleObject
{
    [SerializeField] Vector3 endPos;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 endRot;
    [SerializeField] float rotSpeed;
    [SerializeField] float soundDelay;
    [SerializeField] AudioClip moveSound;
    private AudioSource audioS;
    private bool move = false;
    // Checks if it has been activated before or not
    private bool hasActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        try
        {
            audioS = GetComponent<AudioSource>();
        }
        catch (Exception e)
        {
            audioS = null;
        }
    }

    public override void activate()
    {
        print("Moving " + gameObject.name);
        if (!hasActivated)
        {
            move = true;
            hasActivated = true;
            if(audioS != null && moveSound != null) audioS.PlayOneShot(moveSound);
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (move)
        {
            if (transform.localPosition != endPos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, moveSpeed * Time.deltaTime);
            }

            if(transform.localEulerAngles != endRot)
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, endRot, rotSpeed * Time.deltaTime);
            }
            //audioS.PlayDelayed(soundDelay);
            

            if (transform.localPosition == endPos && transform.localEulerAngles == endRot)
            {
                move = false;

            }
        }
    }


}
