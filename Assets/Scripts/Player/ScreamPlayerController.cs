using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreamPlayerController : MonoBehaviour
{
    //[SerializeField] private AudioSource audioSource;
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CheckAudioVolume volumeDetection;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform cameraOffsetTransform;

    [Header("Volume")]
    [SerializeField] private float volumeSensitivity = 100;
    [SerializeField] private float threshold = 0.1f;

    [Header("Movement")]
    [SerializeField] private int minSpeed;
    [SerializeField] private int maxSpeed;
    [SerializeField] private float movementSpeed;

    [Header("Stats")]
    [SerializeField] private int maxHealth;

    [Header("Debug")]
    [SerializeField] private float debugVolume;
    [SerializeField] private bool debugEnabled;
    

    private Vector2 Velocity;
    private bool isFacingRight;
    public int currentHealth;
    private bool hitWall = true;
    Vector3 tempTransform;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //Time.timeScale = 1;
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

        if (volume > 0.5 && hit2d.collider && !debugEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp(volume, 0, 1) /*debugVolume*/));
            animator.SetTrigger("Jump");

            Debug.Log(Mathf.Clamp(volume, 0, 1));
            //Invoke("ResetJump", 10f);
        }
        else if (Input.GetButtonDown("Jump") && hit2d.collider && debugEnabled)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(minSpeed, maxSpeed, debugVolume));
            animator.SetTrigger("Jump");
        }
        
        //Debug.Log(hit2d.collider != null);

        Flip();
        OffsetCamera();
    }

    private void ResetJump()
    {
        animator.ResetTrigger("Jump");
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(Velocity.x * movementSpeed, rb.velocity.y);

        if (Input.GetButton("Horizontal"))
        {
            animator.SetBool("bIsWalking", true);
        }
        else
        {
            animator.SetBool("bIsWalking", false);
        }
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

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            
            animator.SetTrigger("Die");
            Invoke("Kill", 1);
        }
    }

    private void Kill()
    {
        Destroy(gameObject);
    }

    private void OffsetCamera()
    {

    }
    private void OnDrawGizmos()
    {

    }
}
