using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngineInternal;

// A platform/block that breaks after a certain amount of time once something is placed on top of it or the player walks over it
public class BreakablePlatform : MonoBehaviour
{
    // Broken block to replace platform
    [SerializeField] private GameObject fracturedBlock;
    // Scale factor to ensure it scales to the correct size as the original object - defaulted to 0.5 to match a default cube
    [SerializeField] private float scaleFactor = 0.5f;
    
    // How long until it breaks after being stepped on
    [SerializeField] private float breakCountdown;
    private bool countdownStart = false;
    public bool paused = false;


    // When player steps on a platform or a heavy enough object lands on it (enters the trigger box) then start the timer
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") || other.GetComponent<Rigidbody>().mass > 1)
        {
            countdownStart = true;
        }

    }
    
    // Update is called once per frame
    void Update()
    {
        if (countdownStart && !paused)
        {
            breakCountdown -= Time.deltaTime;
            if(breakCountdown <= 0)
            {

                GameObject broken = Instantiate(fracturedBlock, transform.position, transform.rotation);
                broken.transform.localScale = new Vector3(this.transform.localScale.x * scaleFactor, this.transform.localScale.y * scaleFactor, this.transform.localScale.z * scaleFactor);

                Destroy(gameObject);
            }
        }
    }
}
