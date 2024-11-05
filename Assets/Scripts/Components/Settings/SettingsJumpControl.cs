using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsJumpControl : MonoBehaviour
{
    private float testSensitivity;
    [SerializeField] private CheckAudioVolume volumeDetection;
    [SerializeField] private float threshold = 0.1f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float volume = volumeDetection.GetMicrophoneVolume() * testSensitivity;

        if (volume < threshold)
        {
            volume = 0;
        }

        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, layerMask);

        if (volume > 5 && hit2d.collider != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Lerp(minSpeed, maxSpeed, Mathf.Clamp(volume, 0, 1)));
        }
        Debug.Log(Mathf.Lerp(0, maxSpeed, volume));
    }

    public void SetTestSensitivity(float sensitivity)
    {
        testSensitivity = sensitivity;
    }
}
