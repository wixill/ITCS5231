using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private Image standardIcon;
    [SerializeField] private Image standardBackground;
    [SerializeField] private Image grappleIcon;
    [SerializeField] private Image grappleBackground;
    [SerializeField] private GameObject grappleObject;
    [SerializeField] private Image freezeIcon;
    [SerializeField] private Image freezeBackground;
    [SerializeField] private GameObject freezeObject;
    [SerializeField] private Image flameIcon;
    [SerializeField] private Image flameBackground;
    [SerializeField] private GameObject flameObject;
    [SerializeField] private Image freezeImage;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    private bool screenFreezing;
    private bool isPaused;
    private float fadeInStandard;
    private float fadeInGrapple;
    private float fadeInFreeze;
    private float fadeInFlame;
    private float activeAlpha;
    private float inactiveAlpha;
    private Image activeArrow;

    public static UIManager getInstance() {
        return instance;
    }

    private void Awake()
    {
        audioMixer.GetFloat("Volume", out float vol);
        volumeSlider.SetValueWithoutNotify(vol);
        screenFreezing = false;
        isPaused = false;
        activeArrow = standardBackground;
        activeAlpha = standardBackground.color.a;
        inactiveAlpha = grappleBackground.color.a;
        fadeInStandard = 0;
        fadeInGrapple = 0;
        fadeInFreeze = 0;
        fadeInFlame = 0;
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void ActivatePause() {
        freezeImage.gameObject.SetActive(false);
        isPaused = true;
        Time.timeScale = 0;
        AudioListener.pause = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenuUI.SetActive(true);
    }

    public void ResumeGame() {
        if (screenFreezing) freezeImage.gameObject.SetActive(true);
        isPaused = false;
        Time.timeScale = 1;
        AudioListener.pause = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenuUI.SetActive(false);
    }

    public void RestartLevel() {
        ResumeGame();
        string currentSceen = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceen);
    }

    public void QuitGame() {
        if (Application.isEditor)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
        else
        {
            Application.Quit();
        }
    }

    public void FreezeScreen() {
        freezeImage.gameObject.SetActive(true);
        screenFreezing = true;
    }

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        if (Screen.fullScreen)
        {
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void Update()
    {
        if (screenFreezing) {
            Color tempColor = freezeImage.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1, Time.deltaTime);
            freezeImage.color = tempColor;
            if (tempColor.a >= 0.9) {
                StartCoroutine(PauseForScreenFreeze());
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused) {
            ActivatePause();
        }


        if (fadeInStandard != 0) {
            Color tempColor = standardIcon.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1, Time.deltaTime / fadeInStandard);
            float dif = 1 - tempColor.a;
            if (dif <= 0.1) {
                fadeInStandard = 0;
                tempColor.a = 1;
            }
            standardIcon.color = tempColor;
        }
        if (fadeInGrapple != 0)
        {
            Color tempColor = grappleIcon.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1, Time.deltaTime / fadeInGrapple);
            float dif = 1 - tempColor.a;
            if (dif <= 0.1)
            {
                fadeInGrapple = 0;
                tempColor.a = 1;
            }
            grappleIcon.color = tempColor;
        }
        if (fadeInFreeze != 0)
        {
            Color tempColor = freezeIcon.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1, Time.deltaTime / fadeInFreeze);
            float dif = 1 - tempColor.a;
            if (dif <= 0.1)
            {
                fadeInFreeze = 0;
                tempColor.a = 1;
            }
            freezeIcon.color = tempColor;
        }
        if (fadeInFlame != 0)
        {
            Color tempColor = flameIcon.color;
            tempColor.a = Mathf.Lerp(tempColor.a, 1, Time.deltaTime / fadeInFlame);
            float dif = 1 - tempColor.a;
            if (dif <= 0.1)
            {
                fadeInFlame = 0;
                tempColor.a = 1;
            }
            flameIcon.color = tempColor;
        }
    }

    public void EnableFreeze() {
        freezeObject.SetActive(true);
    }

    public void EnableFlame() {
        flameObject.SetActive(true);
    }

    public void EnableGrapple() {
        grappleObject.SetActive(true);
    }

    public void HideStandardIcon() {
        Color tempColor = standardIcon.color;
        tempColor.a = 0;
        standardIcon.color = tempColor;
    }

    public void HideGrappleIcon()
    {
        Color tempColor = grappleIcon.color;
        tempColor.a = 0;
        grappleIcon.color = tempColor;
    }

    public void HideFreezeIcon()
    {
        Color tempColor = freezeIcon.color;
        tempColor.a = 0;
        freezeIcon.color = tempColor;
    }

    public void HideFlameIcon()
    {
        Color tempColor = flameIcon.color;
        tempColor.a = 0;
        flameIcon.color = tempColor;
    }

    public void FadeInStandardIcon(float duration)
    {
        fadeInStandard = duration;
    }

    public void FadeInGrappleIcon(float duration)
    {
        fadeInGrapple = duration;
    }

    public void FadeInFreezeIcon(float duration)
    {
        fadeInFreeze = duration;
    }

    public void FadeInFlameIcon(float duration)
    {
        fadeInFlame = duration;
    }

    public void SetActive(ArrowType type) {
        Color tempColor = activeArrow.color;
        tempColor.a = inactiveAlpha;
        activeArrow.color = tempColor;
        switch (type) {
            case ArrowType.Standard:
                Color standardColor = standardBackground.color;
                standardColor.a = activeAlpha;
                standardBackground.color = standardColor;
                activeArrow = standardBackground;
                break;
            case ArrowType.Grapple:
                Color grappleColor = grappleBackground.color;
                grappleColor.a = activeAlpha;
                grappleBackground.color = grappleColor;
                activeArrow = grappleBackground;
                break;
            case ArrowType.Freeze:
                Color freezeColor = freezeBackground.color;
                freezeColor.a = activeAlpha;
                freezeBackground.color = freezeColor;
                activeArrow = freezeBackground;
                break;
            case ArrowType.Flame:
                Color flameColor = flameBackground.color;
                flameColor.a = activeAlpha;
                flameBackground.color = flameColor;
                activeArrow = flameBackground;
                break;
        }
    }

    IEnumerator PauseForScreenFreeze() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
