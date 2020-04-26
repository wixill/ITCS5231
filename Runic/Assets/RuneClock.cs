using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneClock : MonoBehaviour
{
    [SerializeField] float colorChangeRate = 0.005f;

    private Renderer[] renderers;
    private Color startColor;
    private Color pulseColor;
    private Color targetColor;
    private bool canPulse;
    private float pulseRate;
    private float lerpt;

    private void Awake()
    {
        pulseColor = Color.black;
        renderers = GetComponentsInChildren<Renderer>();
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
        ChangeColor();
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
