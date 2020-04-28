using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRune_key : PuzzleObject
{
    // Parent IceRuneManager object
    [SerializeField] GameObject iceRuneManager;
    // TorchKeyManager component
    private IceRuneManager rm;
    // Color when button is off
    [SerializeField] private Color offColor;
    // Color when button is on
    [SerializeField] private Color onColor;

    private Renderer[] rend;
    private AudioSource audioS;

    private void Start()
    {
        rm = iceRuneManager.GetComponent<IceRuneManager>();

        rend = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].material.SetColor("_EmissionColor", offColor);
        }
        audioS = gameObject.GetComponent<AudioSource>();
    }

    public override void activate()
    {
        rm.decrementRunes();
        audioS.PlayOneShot(audioS.clip, 1.0f);

        if (rend[0].material.GetColor("_EmissionColor") == offColor)
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material.SetColor("_EmissionColor", onColor);
            }
        }
        else
        {
            for (int i = 0; i < rend.Length; i++)
            {
                rend[i].material.SetColor("_EmissionColor", offColor);
            }
        }
    }
}
