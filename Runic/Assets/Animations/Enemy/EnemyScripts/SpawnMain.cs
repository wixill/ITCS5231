using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMain : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Transform spawnPos;
    private GameObject a;
 

    private bool spawned = false;

    private float turnSpeed;

    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn1());
    }

    void Update()
    {
        
        
            if (spawned)
            {
           

                a.transform.LookAt(Playertrans.position);
            }
            

        
        
    }

    // Update is called once per frame
    IEnumerator Spawn1()
    {
        yield return new WaitForSeconds(1f);
        a = Instantiate(enemy) as GameObject;
        a.transform.position = spawnPos.position;
        a.transform.LookAt(Playertrans.position);
        spawned = true;

    }



}
