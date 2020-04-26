using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : MonoBehaviour
{

    /*
     * Ignights the torch by activating its children
     * Its children are two particle systems, Flame and Light
     */
    public bool lightTorch()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        return true;
    }
}
