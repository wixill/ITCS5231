﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneClock : MonoBehaviour
{
    [SerializeField] float colorChangeRate = 0.005f;
    [SerializeField] float alarm = 50f;
    [SerializeField] ArrowInteraction interact;

    private Renderer[] renderers;
    private Color startColor;
    private Color pulseColor;
    private Color targetColor;
    private float pulseRate;
    private float lerpt;
    private bool alarmActive;
    private bool spedUp;

    private void Awake()
    {
        alarmActive = true;
        spedUp = false;
        pulseColor = Color.black;
        try {
            renderers = GetComponentsInChildren<Renderer>();
        }  catch (MissingComponentException e) {}
        startColor = renderers[0].material.GetColor("_EmissionColor");
        targetColor = startColor;
        pulseRate = -1f;
        lerpt = 1f;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!interact.IsFrozen()) {
            alarm -= Time.deltaTime;
            if (alarm <= 10 && !spedUp) {
                spedUp = true;
                colorChangeRate *= 2;
            } else if (alarm <= 0 && alarmActive) {
                alarmActive = false;
                AlarmEnd();
            }
            ChangeColor();
        }
    }

    private void AlarmEnd() {
        UIManager.getInstance().FreezeScreen();
    }

    private void ChangeColor() {
        lerpt += colorChangeRate * pulseRate;
        if (lerpt <= 0) {
            pulseRate = 1;
        } else if (lerpt >= 1) {
            pulseRate = -1;
        }
        Color newColor = Color.Lerp(startColor, pulseColor, lerpt);
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.SetColor("_EmissionColor", newColor);
        }
    }
}
