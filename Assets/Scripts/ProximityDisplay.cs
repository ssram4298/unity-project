using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ProximityDisplay : MonoBehaviour
{
    public GameObject canvasObject; // Assign the Canvas GameObject in the inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is a hand
        if (other.gameObject.CompareTag("Player")) // Make sure to assign the tag to your hands
        {
            canvasObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is a hand
        if (other.gameObject.CompareTag("HandController"))
        {
            canvasObject.SetActive(false);
        }
    }
}
