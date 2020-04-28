using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn2ndStartDialogue : MonoBehaviour
{
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Transform spawnPos;
    private bool wespawwned = false;
    public GameObject dialogue;

    void Update()
    {
        if (!wespawwned)
        {
            if (Vector3.Distance(Playertrans.position, spawnPos.position) <= 18f)
            {
                StartCoroutine(SpawnD());
                wespawwned = true;

            }
        }


    }

    IEnumerator SpawnD()
    {
        yield return new WaitForSeconds(1f);
        GameObject a = Instantiate(dialogue) as GameObject;


    }
}
