using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu instance;

    private void Awake()
    {
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
    }

    private static MainMenu GetInstance() {
        return instance;
    }

    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame() {
        if (Application.isEditor) {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }

    public void ToggleFullscreen() {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen) {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        } else {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
