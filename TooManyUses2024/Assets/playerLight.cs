using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;  // Make sure to include this for Light2D

public class playerLight : MonoBehaviour
{
    public Attack2 attack2;
    private Light2D playerLight2D;  // Reference to the Light2D component

    private Color normalColor = Color.white;  // Default color
    private Color fireColor = Color.red;// new Color(1f, 0.647f, 0f);   // Orange color (RGB: 255, 165, 0)
    private float normalIntensity = 0f;  // Default intensity
    private float fireIntensity = 1.5f;    // Intensity when in orange
    private float normalRadius;  // Default radius
    private float fireRadius;    // Radius when in orange

    private void Start()
    {
        // Get the Light2D component attached to the same GameObject
        playerLight2D = GetComponent<Light2D>();

        // Store the normal radius
        normalRadius = playerLight2D.pointLightOuterRadius;

        // Set the fire radius to double the normal radius
        fireRadius = normalRadius * 2;

        // Set the initial light properties
        playerLight2D.intensity = normalIntensity;
        playerLight2D.pointLightOuterRadius = normalRadius;
    }

    private void Update()
    {
        if (attack2.cooldownReady)
        {
            // Change the light color to orange, increase the intensity, and double the radius
            playerLight2D.color = fireColor;
            playerLight2D.intensity = fireIntensity;
            playerLight2D.pointLightOuterRadius = fireRadius;
        }
        else
        {
            // Revert the light color, intensity, and radius to their normal values
            playerLight2D.color = normalColor;
            playerLight2D.intensity = normalIntensity;
            playerLight2D.pointLightOuterRadius = normalRadius;
        }
    }
}
