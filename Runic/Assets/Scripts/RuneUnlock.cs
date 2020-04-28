using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class RuneUnlock : MonoBehaviour
{

    public enum RuneType { Freeze, Flame, Grapple }
    public RuneType type;
    private AudioSource audioS;
    private Renderer[] rend;
    private Light plight;

    private void Awake()
    {
        audioS = GetComponent<AudioSource>();
        rend = GetComponentsInChildren<MeshRenderer>();
        plight = GetComponentInChildren<Light>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            PlayerController player = PlayerController.getInstance();

            audioS.PlayOneShot(audioS.clip, 1.0f);
            for(int i = 0; i < rend.Length; i++)
            {
                rend[i].enabled = false;
            }
            plight.enabled = false;

            switch(type) {
                case RuneType.Flame:
                    player.UnlockFlame();
                    break;
                case RuneType.Freeze:
                    player.UnlockFreeze();
                    break;
                case RuneType.Grapple:
                    player.UnlockGrapple();
                    break;
            }
            Destroy(gameObject, 5f);
        }
    }
}
