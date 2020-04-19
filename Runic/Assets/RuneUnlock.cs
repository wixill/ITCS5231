using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneUnlock : MonoBehaviour
{

    public enum RuneType { Freeze, Flame, Grapple }
    public RuneType type;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            PlayerController player = PlayerController.getInstance();
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
            Destroy(gameObject);
        }
    }
}
