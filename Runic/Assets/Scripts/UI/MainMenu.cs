using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;
    private static bool gameStart;

    private void Awake()
    {
        gameStart = false;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if (gameStart) {
            PlayGame();
        }
    }

    private static MainMenu GetInstance() {
        return instance;
    }

    public void PlayGame() {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        gameStart = true;
    }

    public void QuitGame() {
        if (Application.isEditor) {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }
}
