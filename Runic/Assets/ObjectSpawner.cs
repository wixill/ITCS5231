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
        Instantiate(spawnObject, transform.position, Quaternion.identity);
    }
}
