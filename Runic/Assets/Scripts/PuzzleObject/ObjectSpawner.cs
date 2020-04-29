using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : PuzzleObject
{

    [SerializeField] GameObject spawnObject;
    [SerializeField] float spawnDelay = 3f;

    public override void activate()
    {
        StartCoroutine(WaitToSpawnObject());
    }

    IEnumerator WaitToSpawnObject() {
        yield return new WaitForSeconds(spawnDelay);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y - 3, transform.position.z);
        Instantiate(spawnObject, spawnPos, Quaternion.identity);
    }
}
