using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private SceneChanger sceneChanger;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip toggleSound;
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
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.lockState = CursorLockMode.Locked;
        sceneChanger.FadeOut();
    }

    public void QuitGame() {
        if (Application.isEditor) {
            UnityEditor.EditorApplication.isPlaying = false;
        } else {
            Application.Quit();
        }
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("Volume", volume);
    }

    public void ToggleFullscreen() {
        audioSource.PlayOneShot(toggleSound);
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen) {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        } else {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }
}
