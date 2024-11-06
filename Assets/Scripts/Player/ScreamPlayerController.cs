using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreamPlayerController : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CheckAudioVolume volumeDetection;
    [SerializeField] private float volumeSensitivity = 100;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;
    [SerializeField] private float movementSpeed;
    [SerializeField] private LayerMask layerMask;

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

        AdjustSensitivity();

        float volume = volumeDetection.GetMicrophoneVolume() * volumeSensitivity;

        if (volume < threshold)
        {
            volume = 0;
        }

        Debug.Log(Mathf.Clamp(volume, 0, 1));

        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, layerMask);

        if (volume > 0.02 && hit2d.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp(volume, 0, 1)));
        }

        
        //transform.localScale = Vector2.Lerp(minScale, maxScale, volume);
        //прыгать, если звук выше определённого значения 
        //добавить настройку громкости крика 
    }
    private void FixedUpdate()
    {
        
        //Mathf.Lerp(minSpeed, maxSpeed, volume)
        rb.velocity = new Vector2(Velocity.x * maxSpeed, rb.velocity.y);

        transform.position += new Vector3(movementSpeed * Time.deltaTime, 0, 0);
    }

    public void AdjustSensitivity()
    {
        //volumeSensitivity = slider.value;
    }
}
