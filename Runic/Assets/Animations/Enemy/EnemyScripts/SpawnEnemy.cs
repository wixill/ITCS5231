using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private Transform spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn1());
    
    }

    // Update is called once per frame
   IEnumerator Spawn1()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(enemy, spawnPos.position, Quaternion.identity);
    }

    
}
