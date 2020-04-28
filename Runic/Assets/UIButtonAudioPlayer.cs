﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIButtonAudioPlayer : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonHighlight;
    [SerializeField] AudioClip buttonPress;

    private void Awake()
    {
        audioSource.ignoreListenerPause = true;
        //audioSource.ignoreListenerVolume = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.PlayOneShot(buttonPress);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.PlayOneShot(buttonHighlight);
    }
}
