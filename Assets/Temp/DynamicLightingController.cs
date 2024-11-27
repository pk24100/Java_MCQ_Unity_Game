using UnityEngine;

public class DynamicLightingController : MonoBehaviour
{
    public Light pointLight;  // Assign your Point Light or any other light in the Inspector
    public float intensityOnInteraction = 2f;  // Intensity when an action occurs
    public float defaultIntensity = 0.5f;  // Normal intensity

    void Start()
    {
        // Set the default light intensity at the start
        pointLight.intensity = defaultIntensity;
    }

    // Call this function when the user selects a correct answer or interacts with the code
    public void HighlightOnCorrectAnswer()
    {
        pointLight.color = Color.green;  // Change color to green for correct answer
        pointLight.intensity = intensityOnInteraction;  // Increase brightness
    }

    // Call this function for an incorrect answer
    public void HighlightOnError()
    {
        pointLight.color = Color.red;  // Change color to red for error
        pointLight.intensity = intensityOnInteraction;  // Increase brightness
    }

    // Reset the lighting to default after a delay
    public void ResetLighting()
    {
        pointLight.color = Color.white;  // Default color
        pointLight.intensity = defaultIntensity;  // Default intensity
    }
}
