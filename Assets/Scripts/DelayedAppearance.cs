using UnityEngine;
using System.Collections;

public class DelayedAppearance : MonoBehaviour
{
    [SerializeField] private GameObject objectToAppear; // Reference to the GameObject to appear
    [SerializeField] private float delay = 15f; // Delay before the GameObject appears

    void Start()
    {
        if (objectToAppear != null)
        {
            // Initially set the object to not be visible
            objectToAppear.SetActive(false);

            // Start the coroutine to show the object after a delay
            StartCoroutine(ShowObjectAfterDelay(delay));
        }
    }

    IEnumerator ShowObjectAfterDelay(float delaySeconds)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delaySeconds);

        // Activate the GameObject
        objectToAppear.SetActive(true);
    }
}
