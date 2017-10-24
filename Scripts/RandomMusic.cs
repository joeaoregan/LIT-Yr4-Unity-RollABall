/*
 * Joe O'Regan
 * K00203642
 * 
 * Created 24/10/2017
 * 
 * RandomMusic.cs
 * 
 * Select a random background track to play, and constantly select a random track
 * Tested and working with short audio clips
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour {

    public AudioSource track1;
    public AudioSource track2;

	// Use this for initialization
	void Start () {
        PickTrack();
	}
	
	// Update is called once per frame
	void Update () {
        if (!track1.isPlaying && !track2.isPlaying) PickTrack();    // If neither audiosource is playing, pick a new one
	}

    void PickTrack()
    {
        int chooseTune = Random.Range(1, 3);    // max is exclusive, so number will be 1 or 2
        if (chooseTune == 1)
        {
            track1.Play();
        }
        else
        {
            track2.Play();
        }
    }
}
