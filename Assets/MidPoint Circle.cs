using UnityEngine;
using System.Collections.Generic;

public class MidPointCircle : MonoBehaviour
{
    public int radius = 50;
    public Color circleColor = Color.blue;
    public float timerDuration = 100f;
    private float timer;
    private List<Vector2> circlePoints;
    private GUIStyle style;
    private Texture2D texture;

    void Start()
    {
        timer = timerDuration;
        circlePoints = new List<Vector2>();
        DrawCircle();

        // Set up GUI style and texture
        style = new GUIStyle();
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleCenter;  // Align text to the center
        style.normal.textColor = Color.white;

        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, circleColor);
        texture.Apply();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
        }
    }

    void OnGUI()
    {
        // Draw the circle points
        foreach (Vector2 point in circlePoints)
        {
            GUI.DrawTexture(new Rect(point.x, point.y, 1, 1), texture);
        }

        // Calculate the circle's center (top-right corner) with slight upward adjustment
        Vector2 circleCenter = new Vector2(Screen.width - radius - 10, radius + 5);

        // Draw the timer at the adjusted center position
        GUI.Label(new Rect(circleCenter.x - 50, circleCenter.y - 20, 100, 50), timer.ToString("F2"), style);
    }

    void DrawCircle()
    {
        int x = radius;
        int y = 0;
        int decisionOver2 = 1 - x;

        while (y <= x)
        {
            AddCirclePoints(x, y);
            y++;
            if (decisionOver2 <= 0)
            {
                decisionOver2 += 2 * y + 1;
            }
            else
            {
                x--;
                decisionOver2 += 2 * (y - x) + 1;
            }
        }
    }

    void AddCirclePoints(int x, int y)
    {
        // Adjust center to the top-right corner
        int centerX = Screen.width - radius - 10;  // Circle center X
        int centerY = radius + 10;  // Circle center Y

        // Add all eight symmetric points of the circle
        circlePoints.Add(new Vector2(x + centerX, y + centerY));
        circlePoints.Add(new Vector2(-x + centerX, y + centerY));
        circlePoints.Add(new Vector2(x + centerX, -y + centerY));
        circlePoints.Add(new Vector2(-x + centerX, -y + centerY));
        circlePoints.Add(new Vector2(y + centerX, x + centerY));
        circlePoints.Add(new Vector2(-y + centerX, x + centerY));
        circlePoints.Add(new Vector2(y + centerX, -x + centerY));
        circlePoints.Add(new Vector2(-y + centerX, -x + centerY));
    }
}
