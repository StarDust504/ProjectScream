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
    [SerializeField] private float damageDuration;
    [SerializeField] private Image[] hpArray;

    [Header("Debug")]
    [SerializeField] private float debugVolume;
    [SerializeField] private bool debugEnabled;
    

    private Vector2 Velocity;
    private bool isFacingRight;
    public int currentHealth;
    private bool hitWall = true;
    Vector3 tempTransform;
    private bool allowMovement = true;

    private Vector2 lastCheckpointLocation;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Application.targetFrameRate = 144;
        lastCheckpointLocation = transform.position;
        //Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMovement)
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
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
        
    }

    private void ResetJump()
    {
        animator.ResetTrigger("Jump");
    }
    private void FixedUpdate()
    {
        if (allowMovement)
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
        else
        {
            rb.velocity = new Vector2(0, 0);
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

    public void TakeDamage()
    {
        currentHealth -= 1;

        Color tempCol = hpArray[currentHealth].color;
        tempCol.a = 0;
        hpArray[currentHealth].color = tempCol;

        if (currentHealth <= 0)
        {
            allowMovement = false;
            animator.SetTrigger("Die");

            Kill();
        }
        Debug.Log(currentHealth);
    }

    public void SetLastCheckpointLocation(Vector2 location)
    {
        lastCheckpointLocation = location;
    }
    public void Kill()
    {
        foreach (Image tempImg in hpArray)
        {
            Color tempColor = tempImg.color;
            tempColor.a = 1;
            tempImg.color = tempColor;
        }
        animator.SetTrigger("Idle");
        allowMovement = true;
        transform.position = lastCheckpointLocation;
        currentHealth = maxHealth;
        //Destroy(gameObject);
    }

    public void StartDamage()
    {
        InvokeRepeating("TakeDamage", 0, damageDuration);
    }

    public void StopDamage()
    {
        CancelInvoke("TakeDamage");
    }

    private void OffsetCamera()
    {

    }
    private void OnDrawGizmos()
    {

    }
}
