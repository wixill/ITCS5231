using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMain : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private Transform Playertrans;
    [SerializeField] private Transform spawnPos;
    private AudioSource audioSource;
    private GameObject a;

    private bool wait = false;
    private bool spawned = false;

    private float turnSpeed;

    //Vector that will hold the direction of the target
    Vector3 seeTarget;

    //Quaternion needed to help rotate the character
    Quaternion pRotate;
    // Start is called before the first frame update

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
        
        StartCoroutine(Spawn1());
        audioSource.time = 15f;
        audioSource.Play();
        audioSource.SetScheduledEndTime(AudioSettings.dspTime + (20f - 15f));
      


    }

    void Update()
    {
        if (a != null)
        {
           
          if (Vector3.Distance(Playertrans.position, a.transform.position) <= 18f){
                
                StartCoroutine(Wait());
                
                if (spawned)
                {
                   
                    Destroy(a);
                }


            }
        }
    }


    // Update is called once per frame
    IEnumerator Spawn1()
    {
        yield return new WaitForSeconds(1f);
        a = Instantiate(enemy) as GameObject;
        a.transform.position = spawnPos.position;
        a.transform.LookAt(Playertrans.position);
        

    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        spawned = true;
    }
    
   



}
