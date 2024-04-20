using UnityEngine;

public class DisplayCanvas : MonoBehaviour
{
    public GameObject canvasObject; // Assign this in the inspector

    // This function is called when the object is clicked by the mouse
    void OnMouseDown()
    {
        // Check if the Canvas is not active, then activate it
        if (canvasObject != null && !canvasObject.activeInHierarchy)
        {
            canvasObject.SetActive(true);
        }
        else
        {
            // Optionally hide the canvas if it's already visible
            canvasObject.SetActive(false);
        }
    }
}
