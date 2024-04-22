using UnityEngine;

public class TriggerPopup : MonoBehaviour
{
    public GameObject popupCanvas;  // Assign this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // Make sure the player has a tag "Player"
        {
            popupCanvas.SetActive(true);
        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            popupCanvas.SetActive(false);
        }
    }*/
}
