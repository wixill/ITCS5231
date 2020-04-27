using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWhenActivated : PuzzleObject
{
    [SerializeField] Vector3 endPos;
    [SerializeField] float moveSpeed;
    [SerializeField] Vector3 endRot;
    [SerializeField] float rotSpeed;

    private bool move = false;
    // Checks if it has been activated before or not
    private bool hasActivated = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void activate()
    {
        print("Moving object!");
        if (!hasActivated)
        {
            move = true;
            hasActivated = true;
        }
    }

    // Update is called once per frame

    void Update()
    {
        if (move)
        {

            //print("lerping");
            if(transform.localPosition != endPos)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, endPos, moveSpeed * Time.deltaTime);
            } else
            {
                print("SAME POS");
            }
            //print("loc " + transform.localPosition);

            if(transform.localEulerAngles != endRot)
            {
                transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, endRot, rotSpeed * Time.deltaTime);
            } else
            {
                print("SAME ROT");
            }
            //print("rot " + transform.localEulerAngles);
            if (transform.localPosition == endPos && transform.localEulerAngles == endRot)
            {
                move = false;
                print("STOP");
            }
        }
    }


}
