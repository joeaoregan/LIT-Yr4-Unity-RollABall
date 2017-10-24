/*
 * Joe O'Regan
 * K00203642
 * 
 * CameraController.cs
 * 
 * Move the camera offset from the players position, avoiding rotating the camera
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject player;                                       // Reference to the Player

    private Vector3 offset;                                         // Distance to offset the camera from the players position

	// Use this for initialization
	void Start () {
        offset = transform.position - player.transform.position;    // Initialise the offset to the distance between the camera and the player
	}
	
	// Update is called once per frame
	void LateUpdate () {                                            // Late update runs every frame after everything is done
        transform.position = player.transform.position + offset;    // Set the cameras updated position
	}
}
