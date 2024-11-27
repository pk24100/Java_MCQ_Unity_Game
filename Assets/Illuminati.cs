using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenBlinkEffect : MonoBehaviour
{
    public Color blinkColor = Color.blue; // Color for the blink effect
    public float blinkDuration = 1f;    // Duration for each blink

    private Image screenOverlay;

    void Start()
    {
        // Create a full-screen overlay
        GameObject overlay = new GameObject("ScreenOverlay");
        overlay.transform.SetParent(this.transform);
        overlay.transform.localPosition = Vector3.zero;
        overlay.transform.localScale = Vector3.one;

        screenOverlay = overlay.AddComponent<Image>();
        screenOverlay.color = new Color(0, 0, 0, 0); // Initial color (transparent)

        RectTransform rt = overlay.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        // Attach this script to each button's click event
        Button button = GetComponent<Button>();
        button.onClick.AddListener(BlinkScreen);
    }

    void BlinkScreen()
    {
        StartCoroutine(BlinkEffect());
    }

    IEnumerator BlinkEffect()
    {
        // Show the blink color
        screenOverlay.color = blinkColor;
        yield return new WaitForSeconds(blinkDuration);

        // Hide the blink color
        screenOverlay.color = new Color(0, 0, 0, 0);
    }
}
