using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamPlayerController : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CheckAudioVolume volumeDetection;
    [SerializeField] private float volumeSensitivity = 100;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;

    private Vector2 Velocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

        //Debug.Log(volume);

        

        Velocity.x = Input.GetAxis("Horizontal");
        Velocity.y = Input.GetAxis("Vertical");

        if (Velocity.magnitude > 1)
            Velocity.Normalize();
        //transform.localScale = Vector2.Lerp(minScale, maxScale, volume);
        //прыгать, если звук выше определённого значения 
        //добавить настройку громкости крика 
    }
    private void FixedUpdate()
    {
        float volume = volumeDetection.GetMicrophoneVolume() * volumeSensitivity;

        if (volume < threshold)
        {
            volume = 0;
        }

        Debug.Log(volume);
        //Mathf.Lerp(minSpeed, maxSpeed, volume)
        rb.velocity = new Vector2(Velocity.x * maxSpeed, rb.velocity.y);
    }
}
