﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowInteraction : MonoBehaviour

{
    [Header ("Interaction Behaviors")]
    // Can be broken by a normal arrow
    [SerializeField] private bool breakable;
    // Can be pulled towards the player by a grapple arrow (as opposed to the player being pulled towards it)
    [SerializeField] private bool pullable;
    // Can be ignighted by a flame arrow or another adjacent flame
    [SerializeField] private bool flamable;
    // Can be frozen by a freeze arrow
    [SerializeField] private bool freezable;

    // Breakable Specific Fields //
    [Header ("Breakable Fields")]
    // Broken block to replace platform
    [SerializeField] private GameObject fracturedBlock;
    // Scale factor to ensure it scales to the correct size as the original object - defaulted to 0.5 to match a default cube
    [SerializeField] private float scaleFactor = 0.5f;

    // Pullable Specific Fields //
    [Header ("Pullable Fields")]
    // How fast it gets pulled toward the player
    [SerializeField] private int pullSpeed;

    // Flamable Specific Fields //
    [Header ("Flamable Fields")]
    // Should the particle effect be prewarmed (start at 100%) or start from 0 and build up to full
    [SerializeField] private bool prewarm;
    // What type of fire should eminate from the object
    [SerializeField] private GameObject fireType;
    // Scale factor to ensure the fire isn't a different size than the object
    [SerializeField] private float fireScaleFactor = 1f;
    // Where should the fire's center be
    [SerializeField] private Vector3 fireCenter;
    // Should the fire die out? If so, how quickly? -1 for no, any positive int for how fast it should die out
    [SerializeField] private int burnoutTime;

    // Freezable Specific Fields //
    [Header ("Freezable Fields")]
    // How long until it takes to thaw
    [SerializeField] private int thawTime;


    /**
     * Breaks block into smaller blocks
     */
    public void breakSelf()
    {
        GameObject broken = Instantiate(fracturedBlock, transform.position, transform.rotation);
        broken.transform.localScale = new Vector3(this.transform.localScale.x * scaleFactor, this.transform.localScale.y * scaleFactor, this.transform.localScale.z * scaleFactor);
        Destroy(this.gameObject);
    }

    /**
     * Pulls object towards player
     */
    public void getPulled()
    {

    }

    /*
     * Catches fire
     */
    public void catchFire()
    {
        // Create the fire
        Vector3 fc = transform.position;
        if (fireCenter != Vector3.zero)
        {
            fc = fireCenter;
        }
        GameObject fire = Instantiate(fireType, fc, transform.rotation);
        fire.transform.localScale = new Vector3(this.transform.localScale.x * fireScaleFactor, this.transform.localScale.y * fireScaleFactor, this.transform.localScale.z * fireScaleFactor);

        ParticleSystem firePS = fire.GetComponent<ParticleSystem>();

        if (!prewarm)
        {
            //firePS.
        }
    }

    /*
     * Freezes object in place for a certain amount of time
     */
    public void freeze()
    {

    }


    
}
