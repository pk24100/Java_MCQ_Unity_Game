using UnityEngine;

public class RectangleDrawer : MonoBehaviour
{
    public Vector2Int startPoint;  // Starting point of the rectangle
    public int width = 10;         // Width of the rectangle
    public int height = 5;         // Height of the rectangle

    void Start()
    {
        // Define the four corners of the rectangle
        Vector2Int topRight = new Vector2Int(startPoint.x + width, startPoint.y);
        Vector2Int bottomLeft = new Vector2Int(startPoint.x, startPoint.y - height);
        Vector2Int bottomRight = new Vector2Int(startPoint.x + width, startPoint.y - height);

        // Draw the four edges of the rectangle using Bresenham's line algorithm
        DrawLine(startPoint, topRight);      // Top edge
        DrawLine(startPoint, bottomLeft);    // Left edge
        DrawLine(bottomLeft, bottomRight);   // Bottom edge
        DrawLine(topRight, bottomRight);     // Right edge
    }

    // Bresenham's Line Drawing Algorithm
    void DrawLine(Vector2Int point1, Vector2Int point2)
    {
        int x1 = point1.x;
        int y1 = point1.y;
        int x2 = point2.x;
        int y2 = point2.y;

        int dx = Mathf.Abs(x2 - x1);
        int dy = Mathf.Abs(y2 - y1);
        int sx = (x1 < x2) ? 1 : -1;
        int sy = (y1 < y2) ? 1 : -1;
        int err = dx - dy;

        while (true)
        {
            // Place a point at (x1, y1)
            PlacePixel(new Vector2Int(x1, y1));

            // Check if we have reached the end point
            if (x1 == x2 && y1 == y2) break;

            int e2 = 2 * err;
            if (e2 > -dy)
            {
                err -= dy;
                x1 += sx;
            }
            if (e2 < dx)
            {
                err += dx;
                y1 += sy;
            }
        }
    }

    // Function to visually place a pixel or point (you can replace this with a real drawing method)
    void PlacePixel(Vector2Int point)
    {
        // Draws a debug point in the scene view in Unity
        Debug.Log($"Placing point at: {point.x}, {point.y}");

        // Optional: You could also visualize this in Unity by placing game objects or using textures
        GameObject pixel = GameObject.CreatePrimitive(PrimitiveType.Cube);
        pixel.transform.position = new Vector3(point.x, point.y, 0);
        pixel.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);  // Adjust scale to simulate a "pixel"
    }
}
