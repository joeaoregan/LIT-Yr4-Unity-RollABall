/*
 * Joe O'Regan
 * K00203642
 * 
 * Rotate.cs
 * 
 * Rotate the pick up objects 45 degrees on each axis
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {
    	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime); // Rotate the game object
	}
}
