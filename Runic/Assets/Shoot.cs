using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    private bool isAiming;
    public GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawn;
    [SerializeField] private float shootingForce;
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        isAiming = Input.GetMouseButton(1);
        //print("Spawn: " + arrowSpawn.position);
        if (Input.GetMouseButtonDown(0) && isAiming)
        {
            print("shoot");
            SpawnArrow();
            //if we did not just shoot an arrow or we waited 7 seconds to shoot again
            //if (justShot == false)
            //{
            //Shoot();
            //justShot = true;
            //}
            //if we just shot an arrow
            //else
            //{
            //cooldown time to wait before shooting again 
            //StartCoroutine(WaitTimeForShooting());
            //allows the player to shoot again after cooldown
            //}
        }
    }

    void SpawnArrow()
    {
        //print("Spawn: " + arrowSpawn.position);
        GameObject newArrow = Instantiate(arrowPrefab, arrowSpawn.position, Quaternion.identity);
        //a.transform.position = new Vector3(player.transform.position.x, 2f, player.transform.position.z);
        Rigidbody rb = newArrow.GetComponent<Rigidbody>();
        rb.velocity = cam.transform.forward * shootingForce;
    }
}
