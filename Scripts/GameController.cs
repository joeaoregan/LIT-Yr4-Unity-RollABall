/*
 * Joe O'Regan
 * K00203642
 * 
 * GameController.cs
 * 
 * Add randomly spawning powerups and restart the game after winning/losing
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;                                                  // SceneManager

public class GameController : MonoBehaviour {

    public CountdownTimer gametimer;                                                // Reference to the game timer
    public float restartWait = 3.0f;                                                // Time to wait before restarting the game
    public Text winText;                                                            // Win text message
    public int minPowerUpSpawnTime = 5;                                             // Min time of range to spawn power up
    public int maxPowerUpSpawnTime = 10;                                            // Max time of range to spawn power up

    public GameObject powerup;                                                      // Reference to the timer Power Up

    // Use this for initialization
    void Start ()
    {
        powerup.gameObject.SetActive(false);                                        // Set the Timer Power Up inactive from the start
        winText.text = "";                                                          // Clear the win message text
    }
	
	// Update is called once per frame
	void Update ()
    {   
        // If the time is between 5 and 10 seconds, and is not active
        if (gametimer.currentTime == Random.Range(minPowerUpSpawnTime, maxPowerUpSpawnTime) && !powerup.activeSelf) {  
            powerup.gameObject.SetActive(true);                                     // Set the power up object active
            SetPowerUpPosition();                                                   // Choose a corner for the Timer Power Up to spawn in
        }
        if (gametimer.currentTime == 0)                                             // If current time is 0
        {
            winText.text = "Loser!";                                                // Display loser message
            //RestartGame();
            StartCoroutine(RestartGame());                                          // Start RestartGame() co routine
        }
    }
    
    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(restartWait);                               // Wait the amount set, before restarting the scene

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);           // Reload the scene
    }

    // Randomly select a corner for the power up to spawn in
    void SetPowerUpPosition()
    {
        int corner = Random.Range(1, 5);                                            // Choose a corner for the power up to spawn in (Range(1,5) returns 1,2,3,or 4)

        if (corner == 1)
        {
            //cornerPosition = new Vector3(7.5f, 0.0f, 7.5f);
            powerup.transform.position = new Vector3(7.5f, 0.5f, 7.5f);             // Top right
        }
        else if (corner == 2)
        {
            powerup.transform.position = new Vector3(7.5f, 0.5f, -7.5f);            // Bottom Right
        }
        else if (corner == 3)
        {
            powerup.transform.position = new Vector3(-7.5f, 0.5f, -7.5f);           // Bottom Left
        }
        else
        {
            powerup.transform.position = new Vector3(-7.5f, 0.5f, 7.5f);            // Top Left
        }
    }

    public void AllPickUpsCollected() {
        winText.text = "You Win!";                                                  // Display the win message
        StartCoroutine(RestartGame());                                              // Start RestartGame() co routine
        gametimer.StopCoroutine("LoseTime");                                        // Stop counting down
    }
}
