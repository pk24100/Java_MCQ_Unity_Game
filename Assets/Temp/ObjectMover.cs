using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 50f;
    public Vector3 defaultScale = new Vector3(1, 1, 1);
    public Vector3 enlargedScale = new Vector3(2, 2, 2);

    void Update()
    {
        // Move the object to the right
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // Rotate the object clockwise
        if (Input.GetKey(KeyCode.R))
        {
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }

        // Enlarge the object when "E" is pressed
        if (Input.GetKey(KeyCode.E))
        {
            transform.localScale = enlargedScale;
        }

        // Reset the object's size when "D" is pressed
        if (Input.GetKey(KeyCode.D))
        {
            transform.localScale = defaultScale;
        }
    }
}
