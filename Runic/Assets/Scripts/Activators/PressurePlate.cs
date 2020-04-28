using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Activator
{
    // Color when button is off
    [SerializeField] private Color offColor;
    // Color when button is on
    [SerializeField] private Color onColor;
    // Is reversed if it requires weight to be taken off of it
    [SerializeField] private bool reversed;
    // Dimensions of the box that will detect if something heavy is inside it
    [SerializeField] private Vector3 sensor;
    // Offset for sensor
    [SerializeField] private Vector3 sensorOffset;
    // List of objects inside sensor
    private Collider[] whatsOnTop;
    // Length of whatsOnTop at the start of the scene
    private int startLen;
    private AudioSource audioS;

    // Renderer
    private Renderer[] rend;

    // Checks if it has been activated before or not
    private bool hasActivated = false;
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rend.Length; i++) {
            rend[i].material.SetColor("_EmissionColor", offColor);
        }
        

        if (startOn)
        {
            activate();
            hasActivated = true;
        }
        Vector3 offsetPos = new Vector3(transform.position.x + sensorOffset.x, transform.position.y + sensorOffset.y, transform.position.z + sensorOffset.z);
        whatsOnTop = Physics.OverlapBox(transform.position, sensor);
        startLen = whatsOnTop.Length;
        audioS = gameObject.GetComponent<AudioSource>();
    }
    private void OnDrawGizmosSelected()
    {
        if (reversed)
        {
            Gizmos.color = Color.red;
            Vector3 offsetPos = new Vector3(transform.position.x + sensorOffset.x, transform.position.y + sensorOffset.y, transform.position.z + sensorOffset.z);
            Gizmos.DrawWireCube(offsetPos, sensor);
        }
    }

    private void Update()
    {
        if (reversed)
        {
            if (whatsOnTop.Length < startLen)
            {

                if (!hasActivated)
                {
                    activate();
                    hasActivated = true;
                    for (int i = 0; i < rend.Length; i++)
                    {
                        rend[i].material.SetColor("_EmissionColor", onColor);
                    }
                    audioS.PlayOneShot(audioS.clip, 1.0f);
                }
            }
            else if (!hasActivated)
            {
                whatsOnTop = Physics.OverlapBox(transform.position, sensor);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!reversed)
        {
            if ((!hasActivated || isToggleable) && other.tag == "Interactable")
            {
                if (other.GetComponent<ArrowInteraction>().getPulled())
                {
                    activate();
                    hasActivated = true;
                    for (int i = 0; i < rend.Length; i++)
                    {
                        rend[i].material.SetColor("_EmissionColor", onColor);
                    }
                    audioS.PlayOneShot(audioS.clip, 1.0f);
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (!reversed)
        {
            if (isToggleable)
            {
                rend[0].material.SetColor("_EmissionColor", offColor);
            }
        }
    }
}
