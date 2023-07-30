using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarChartAnimation : MonoBehaviour
{
    public GameObject[] bars; // Array of bar game objects
    public float animationDuration = 2f; // Duration of the animation in seconds
    public float maxHeight = 10f; // Maximum height for the bars

    private float[] targetHeights; // Target heights for the bars
    private float[] initialHeights; // Initial heights of the bars
    private float animationTimer; // Timer for the animation

    private void Start()
    {
        // Initialize the arrays
        targetHeights = new float[bars.Length];
        initialHeights = new float[bars.Length];

        // Set initial heights and target heights for the bars
        for (int i = 0; i < bars.Length; i++)
        {
            targetHeights[i] = Random.Range(0f, maxHeight); // Set target height randomly (replace with your data logic)
            initialHeights[i] = 0f; // Set initial height to 0

            // Set initial position and scale of the bars
            bars[i].transform.localScale = new Vector3(1f, initialHeights[i], 1f);
            bars[i].transform.localPosition = new Vector3(i, initialHeights[i] / 2f, 0f);
        }

        // Start the animation
        animationTimer = 0f;
    }

    private void Update()
    {
        // Update the animation timer
        animationTimer += Time.deltaTime;

        // Animate the bars
        for (int i = 0; i < bars.Length; i++)
        {
            float t = Mathf.Clamp01(animationTimer / animationDuration); // Get the interpolation value

            // Interpolate the height based on the target height and animation timer
            float height = Mathf.Lerp(initialHeights[i], targetHeights[i], t);

            // Update the scale and position of the bars
            bars[i].transform.localScale = new Vector3(1f, height, 1f);
            bars[i].transform.localPosition = new Vector3(i, height / 2f, 0f);
        }

        // Reset the animation
        if (animationTimer >= animationDuration)
        {
            animationTimer = 0f;
            for (int i = 0; i < bars.Length; i++)
            {
                initialHeights[i] = targetHeights[i]; // Set the new initial height to the previous target height
                targetHeights[i] = Random.Range(0f, maxHeight); // Set a new target height randomly (replace with your data logic)
            }
        }
    }
}
