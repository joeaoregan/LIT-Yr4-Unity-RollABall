/*
 * Joe O'Regan
 * K00203642
 * 
 * Created 24/10/2017
 * 
 * AudioController.cs
 * 
 *      Select a random background track to play, and constantly select a random track
 *      Tested and working with short audio clips
 * 
 * 29/10/2017:
 *      Added singleton, so audio can continue playing between scenes, 
 *      and not reset when the scene resets
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{

    public AudioSource track1;
    public AudioSource track2;

    //public Text trackTimeText;    // Display the current track time

    //string trackTime;

    private static AudioController instance = null;
    public static AudioController Instance
    {
        get { return instance; }
    }

    private void Awake()
    {        
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);

        DontDestroyOnLoad(this.gameObject);

        //trackTimeText.text = TrackTime();
    }

    // Use this for initialization
    void Start()
    {
        PickTrack();
        //trackTimeText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (!track1.isPlaying && !track2.isPlaying) PickTrack();    // If neither audiosource is playing, pick a new one

        //trackTimeText.text = TrackTime();
    }

    void PickTrack()
    {
        int chooseTune = Random.Range(1, 3);                        // max is exclusive, so number will be 1 or 2
        if (chooseTune == 1)
        {
            track1.Play();
        }
        else
        {
            track2.Play();
        }
    }
    /*
    // Display the current track play time
    public string TrackTime()
    {
        if (track1.isPlaying)
            return trackTime = "Track: " + System.Math.Round(track1.time, 1).ToString();    // Round track time to 1 decimal place and display

        return trackTime = "Track: " + System.Math.Round(track2.time, 1).ToString();
    }
    */
}
