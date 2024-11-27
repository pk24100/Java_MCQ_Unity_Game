using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;  

public class ApplyMaterial : MonoBehaviour
{
    public Material uiMaterial;  // Assign this in the inspector
    public List<Image> uiImages; // Assign the UI images in the inspector

    void Start()
    {
        foreach (Image img in uiImages)
        {
            img.material = uiMaterial;
        }
    }
}
