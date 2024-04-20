using UnityEngine;
using TMPro;

public class DisplayCanvasOnText : MonoBehaviour
{
    public TextMeshProUGUI textMesh; // Reference to the TextMeshPro component
    public GameObject canvas;        // Reference to the Canvas GameObject

    void Update()
    {
        // Check if the TextMeshPro text is not empty or if it is enabled and visible
        if (!string.IsNullOrEmpty(textMesh.text) && textMesh.enabled && textMesh.gameObject.activeInHierarchy)
        {
            // Activate the canvas if it's not already active
            if (!canvas.activeSelf)
            {
                canvas.SetActive(true);
            }
        }
        else
        {
            // Optionally hide the canvas if the text is empty or the component is disabled
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
            }
        }
    }
}
