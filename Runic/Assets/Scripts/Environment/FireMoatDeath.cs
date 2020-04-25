using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireMoatDeath : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("DEATH TRIGGERED");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER DIES");
            //string currentSceen = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.CompareTag("Destroyable"))
        {
            Destroy(collision.gameObject);
        }
    }
}
