/*
 * Joe O'Regan
 * K00203642
 * 
 * CountdownTimer.cs
 * 
 * Create a countdown timer in the top right corner of the game screen
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour {

    public int currentTime = 15;                     // Time to start counting down from
    public Text timeText;                           // Text to display the time

    // Use random time to generate time power up
    //private float maxTime;
    //private float minTime;

    //public PlayerController rollABall;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");                 // Start counting down immediately
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = ("Time: " + currentTime);   // Set the timeText

        if (currentTime <= 0)                       // If time has runout
        {
            StopCoroutine("LoseTime");              // Stop counting down
            timeText.text = "Times Up!";            // Set the time ran out message

            //rollABall.OnDisable();
        }
    }

    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);     // Every second
            currentTime--;                          // Decrement the time
        }
    }

    public void TimeBoost() {
        currentTime += 5;                           // Increase the time, after collecting a time power up
    }
}
