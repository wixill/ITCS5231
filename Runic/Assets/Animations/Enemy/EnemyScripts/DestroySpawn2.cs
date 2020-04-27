using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpawn2 : MonoBehaviour
{
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Transform spawnPos;
    private bool wespawwned = false;
    void Update()
    {
        if (!wespawwned)
        {
            if (Vector3.Distance(Playertrans.position, spawnPos.position) <= 30)
            {
                StartCoroutine(Disappear());
                wespawwned = true;

            }
        }


    }
    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
