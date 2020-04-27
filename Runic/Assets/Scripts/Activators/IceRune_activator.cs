using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceRune_activator : Activator
{

    private Renderer[] matRenderers;
    private Material[] normalMats;
    [SerializeField] private Material iceMat;
    // Checks if its been lit or not to prevent toggling
    private bool isFrozen = false;
    // Color when button is off
    [SerializeField] private Color offColor;


    private void Start()
    {
        matRenderers = GetComponentsInChildren<Renderer>();
        if (matRenderers.Length == 0) matRenderers[0] = GetComponent<Renderer>();
        normalMats = new Material[matRenderers.Length];
        for (int i = 0; i < matRenderers.Length; i++)
        {
            normalMats[i] = matRenderers[i].material;
            matRenderers[i].material.SetColor("_EmissionColor", offColor);
        }

    }

    public bool freezeSelf()
    {
        if (!isFrozen)
        {
            activate();

            for (int i = 0; i < matRenderers.Length; i++)
            {
                matRenderers[i].material = iceMat;
            }
        }
        isFrozen = true;
        return true;
    }

}
