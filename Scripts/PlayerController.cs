/*
 * Joe O'Regan
 * K00203642
 * 
 * PlayerController.cs
 * 
 * Control Player Sphere moving it in the scene
 * Add additional sound effect for Timer Power Up (changing the pitch of the original pick up sound)
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.SceneManagement;                                // SceneManager

public class PlayerController : MonoBehaviour {

    public float speed;                                             // Speed to move the Player
    public Text countText;                                          // Number of Pick Ups Collected text
    //public Text winText;                                          // Game Over message    HANDLED IN GAME CONTROLLER

    public CountdownTimer timer;                                    // Reference to the game timer
    public GameController gameController;                           // Referenct to the game controller
    public float restartWait = 5.0f;                                // Time to wait before restarting

    public AudioSource audioPickup;                                 // Pick Up Sound Effect
    public AudioSource audioPowerUp;                                // Sound effect for collecting Timer Power Up (Same effect with pitch changed)

    private Rigidbody rb;                                           // Rigidbody component
    private int count;                                              // Number of objects collected

    private float moveHorizontal;                                   // Move the player on X axis
    private float moveVertical;                                     // Move the player on Y axis

    void Main()
    {
        // Preventing mobile devices going in to sleep mode 
        //(actual problem if only accelerometer input is used)
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();                             // Get the rigid body component
        count = 0;                                                  // Set the count to 0
        SetCountText();                                             // Display the number of objects collected (0 to begin with)
        //winText.text = "";                                        // Blank out the win message    HANDLED IN GAME CONTROLLER

        //audioPickup = GetComponent<AudioSource>();
        //audioPowerUp = GetComponent<AudioSource>();

        // Added - Set movement
        moveHorizontal = 0.0f;                                      // Clear the horizontal movement
        moveVertical = 0.0f;                                        // Clear vertical movement
}
    /*
    private void OnEnable()
    {
        rb.isKinematic = false;                                                         // Allow the player to move

        // Reset the movement
        moveHorizontal = 0.0f;
        moveVertical = 0.0f;
    }
    */
    private void OnDisable()
    {
        rb.isKinematic = true;                                      // Stop the rigidbody moving
    }

    private void CheckTimer()
    {
        if (timer.currentTime == 0)                                 // If the time runs out
        {
            //winText.text = "Loser!";
            OnDisable();                                            // Stop the player
            //RestartGame();
        }
        //else OnEnable();
    }

    private void FixedUpdate() {

        //if (timer.currentTime == 0) speed = 0;  // Stop the player?
        //else speed = 5;
        //if (timer.currentTime == 0) rb.isKinematic = true;  // Stop the player?
        //else rb.isKinematic = false;
        CheckTimer();                                                               // Check the time

        // 13/09/2017 Separate Contrller Input
        if (SystemInfo.deviceType == DeviceType.Desktop)                            // Keyboard Input
        {
            //float 
                moveHorizontal = Input.GetAxis("Horizontal");                       // Get the input on the X axis
            //float 
                moveVertical = Input.GetAxis("Vertical");                           // Get the input on the Z axis

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);     // Set the movment for the player

            rb.AddForce(movement * speed);                                          // Set the force to apply to the Players rigidbody
        }
        /*
        //     else if (SystemInfo.deviceType == DeviceType.Handheld)               // Mobile Input
        else
        {
            //float mobileMoveH = Input.gyro.userAcceleration.x;
            // float mobileMoveY = Input.gyro.userAcceleration.y;

            // Vector3 movement = new Vector3(mobileMoveH, 0.0f, mobileMoveY);
            // Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);

            //  rb.AddForce(movement * speed * Time.deltaTime);
            transform.Translate(Input.acceleration.x, 0.0f, Input.acceleration.z);
            //transform.Translate(Input.acceleration.x, Input.acceleration.y, 0.0f);
        }*/
        else
        {
            // Player movement in mobile devices Building of force vector 
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            rb.AddForce(movement * speed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))             // If the player has collided with a Pick Up
        {
            other.gameObject.SetActive(false);                  // Collect the pick up, and set it inactive
            count += 1;                                         // Increment the number of objects collected
            SetCountText();                                     // Update the count display text

            // Audio
            audioPickup.Play();                                 // Play the pickup sound effect
        }
        else if (other.gameObject.CompareTag("Timer Power Up")) // If the player has collided with a Timer Power Up
        {
            timer.TimeBoost();                                  // Add 5 seconds to the timer
            other.gameObject.SetActive(false);                  // Collect and, set the Timer Power Up inactive
            audioPowerUp.Play();                                // Play the Power Up sound effect
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();          // Display the number of objects collected
        if (count >= 12)
        {
            //winText.text = "You Win!";                        // Display the win message
            OnDisable();                                        // Stop the player moving
            gameController.AllPickUpsCollected();               // The player has won the game
        }
    }
}
