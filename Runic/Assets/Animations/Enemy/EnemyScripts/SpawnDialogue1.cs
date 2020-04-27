using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnDialogue1 : MonoBehaviour
{
    public GameObject dialogue;
  
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnD());

    }

    // Update is called once per frame
    IEnumerator SpawnD()
    {
        yield return new WaitForSeconds(1f);
        GameObject a = Instantiate(dialogue) as GameObject;
 

    }
}
