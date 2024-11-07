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
    [SerializeField] private Animator animator;

    private Vector2 Velocity;
    private bool isFacingRight;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, 0.8f, layerMask);

        if (volume > 0.02 && hit2d.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp(volume, 0, 1)));
            animator.SetTrigger("Jump");
        }
        Debug.Log(hit2d.collider != null);

        Flip();
    }
    private void FixedUpdate()
    {

        rb.velocity = new Vector2(Velocity.x * movementSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (!isFacingRight && Velocity.x < 0 || isFacingRight && Velocity.x > 0)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    public void AdjustSensitivity()
    {
        //volumeSensitivity = slider.value;
    }
}
