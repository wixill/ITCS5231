using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Transform spawnPos;
    private bool wespawwned = false;


    private float turnSpeed;

    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;
    // Start is called before the first frame update
    void Update()
    {
        if (!wespawwned)
        {
            if (Vector3.Distance(Playertrans.position, spawnPos.position) <= 30)
            {
                StartCoroutine(Spawn1());
                wespawwned = true;

            }
        }
    

    }


    // Update is called once per frame
    IEnumerator Spawn1()
    {
        yield return new WaitForSeconds(1f);
        GameObject a = Instantiate(enemy) as GameObject;
        a.transform.position = spawnPos.position;
        a.transform.LookAt(Playertrans.position);
        

    }

}
