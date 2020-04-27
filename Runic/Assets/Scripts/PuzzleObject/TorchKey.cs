using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting;
using UnityEngine;

public class TorchKey : PuzzleObject
{
    // Parent TorchKeyManager object
    [SerializeField] GameObject torchKeyManager;
    // TorchKeyManager component
    private TorchKeyManager tm;
    // LightTorch component
    private LightTorch lt;


    private void Start()
    {
        tm = torchKeyManager.GetComponent<TorchKeyManager>();
        lt = GetComponent<LightTorch>();
    }

    public override void activate()
    {
        tm.decrementTorches();
        lt.lightTorch();
    }


}
