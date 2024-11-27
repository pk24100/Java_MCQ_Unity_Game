using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShiftAndScale : MonoBehaviour
{
    public Button topLeftButton;  // Assign this in the inspector
    public List<RectTransform> uiElements;  // Assign the UI components in the inspector
    public float scaleFactor = 1.25f;  // Scaling factor
    public float moveDistance = 300f;  // Distance to move left

    void Start()
    {
        // Add listener to the button
        topLeftButton.onClick.AddListener(OnTopLeftButtonClick);
    }

    void OnTopLeftButtonClick()
    {
        foreach (RectTransform element in uiElements)
        {
            // Move the element to the left
            element.anchoredPosition += new Vector2(-moveDistance, 0);

            // Scale up the element
            element.localScale *= scaleFactor;
        }
    }
}
