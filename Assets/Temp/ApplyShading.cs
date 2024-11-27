using UnityEngine;
using UnityEngine.UI;

public class ApplyShading : MonoBehaviour
{
    public Image targetImage; // Assign the UI element (like a Button or Panel) in the Inspector
    public Texture2D texture; // Assign a texture through the Inspector
    public Shader customShader; // Optional: Assign a shader through the Inspector for advanced effects

    private Material material; // Material to be created and applied

    void Start()
    {
        // Check if a texture and targetImage are assigned
        if (texture == null || targetImage == null)
        {
            Debug.LogError("Texture or Target Image is not assigned!");
            return;
        }

        // Create a new Material with a custom shader or a default UI shader
        if (customShader != null)
        {
            material = new Material(customShader);
        }
        else
        {
            material = new Material(Shader.Find("UI/Default"));
        }

        // Assign the texture to the material
        material.mainTexture = texture;

        // Adjust shading properties
        material.SetFloat("_Glossiness", 0.5f); // Adjust for different levels of glossiness (0 to 1)
        material.SetFloat("_Metallic", 0.2f);   // Adjust for metallic effect (0 to 1)

        // Apply the material to the UI element
        targetImage.material = material;
    }

    // Optional: Change shading dynamically, for example when hovering over the element
    public void OnMouseHover()
    {
        material.SetFloat("_Glossiness", 0.8f); // Make it shinier on hover
    }

    public void OnMouseExit()
    {
        material.SetFloat("_Glossiness", 0.5f); // Reset glossiness on exit
    }
}

