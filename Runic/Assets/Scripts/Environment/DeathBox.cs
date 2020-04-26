using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathBox : MonoBehaviour
{
    // If player collides with Death Box, restart from checkpoint
    // (Right now just restart the game)
    // TODO: Update to restart to checkpoint
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")){
            Debug.Log("PLAYER DIES");
            //string currentSceen = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        } else if (other.gameObject.CompareTag("Destroyable"))
        {
            Destroy(other.gameObject);
        }
    }
}
