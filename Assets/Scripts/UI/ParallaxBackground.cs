using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public Transform[] layers; // Array to hold your background layers
    public float[] parallaxSpeeds; // Array to control the speed of each layer

    private float startingX; // Store the initial X position of the camera

    void Start()
    {
        // Get the initial X position of the camera
        startingX = Camera.main.transform.position.x;
    }

    void Update()
    {
        // Calculate the camera's movement
        float cameraX = Camera.main.transform.position.x;
        float deltaX = cameraX - startingX;

        // Move each background layer
        for (int i = 0; i < layers.Length; i++)
        {
            // Apply parallax based on speed
            float parallax = deltaX * parallaxSpeeds[i];
            layers[i].position = new Vector3(startingX + parallax, layers[i].position.y, layers[i].position.z);
        }
    }
}

