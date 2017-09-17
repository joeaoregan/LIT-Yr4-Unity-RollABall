using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallController : MonoBehaviour {

    private AudioSource audioBumpWall;

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
