using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float speed;
    public Text countText;
    public Text winText;

    private AudioSource audioPickup;

    private Rigidbody rb;
    private int count;

    void Main()
    {
        // Preventing mobile devices going in to sleep mode 
        //(actual problem if only accelerometer input is used)
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";

        audioPickup = GetComponent<AudioSource>();
    }

    private void FixedUpdate() {
        // 13/09/2017 Separate Contrller Input
        if (SystemInfo.deviceType == DeviceType.Desktop)                                // Keyboard Input
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);


            rb.AddForce(movement * speed);
        }
        /*
        //     else if (SystemInfo.deviceType == DeviceType.Handheld)                          // Mobile Input
        else
        {
            //float mobileMoveH = Input.gyro.userAcceleration.x;
            // float mobileMoveY = Input.gyro.userAcceleration.y;

            // Vector3 movement = new Vector3(mobileMoveH, 0.0f, mobileMoveY);

            // Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);

            //  rb.AddForce(movement * speed * Time.deltaTime);
            transform.Translate(Input.acceleration.x, 0.0f, Input.acceleration.z);
            //transform.Translate(Input.acceleration.x, Input.acceleration.y, 0.0f);
        }
        */
        else
        {
            // Player movement in mobile devices Building of force vector 
            Vector3 movement = new Vector3(Input.acceleration.x, 0.0f, Input.acceleration.y);
            rb.AddForce(movement * speed);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();

            // Audio
            audioPickup.Play();                             // Play the pickup sound effect
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 12)
        {
            winText.text = "You Win!";
        }
    }
}
