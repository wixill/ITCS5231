using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : PuzzleObject
{
    // Color to start as
    [SerializeField] private Color startColor;
    // Color to change into
    [SerializeField] private Color endColor;

    // Cube object
    Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetColor("_Color", startColor);
        //GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //cubeRenderer = cube.GetComponent<Renderer>();
        //cubeRenderer.material.SetColor("_Color", startColor);
    }

    // Activate - changes the color of the object
    public override void activate()
    {
        if(rend.material.GetColor("_Color") == startColor)
        {
            rend.material.SetColor("_Color", endColor);
        } else
        {
            rend.material.SetColor("_Color", startColor);
        }
    }

}
