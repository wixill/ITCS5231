﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnD2 : MonoBehaviour
{
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Transform spawnPos;
    private bool wespawwned = false;
    public GameObject dialogue;

    void Update()
    {
        if (!wespawwned)
        {
            if (Vector3.Distance(Playertrans.position, spawnPos.position) <= 23)
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
