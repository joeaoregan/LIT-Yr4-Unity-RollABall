/*
 * Joe O'Regan
 * K00203642
 * 
 * WallController.cs
 * 
 * Detect collisions with the player object
 * Play a sound when hit
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour {

    private AudioSource audioBumpWall;                                  // Sound effect added for colliding with the wall

    // Use this for initialization
    void Start ()
    {
        audioBumpWall = GetComponent<AudioSource>();
    }
	
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.red);
        }

        if (collision.relativeVelocity.magnitude > 2)
            audioBumpWall.Play();
    }
}
