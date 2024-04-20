using UnityEngine;
using TMPro;
using System.Collections;

public class Keypad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Ans;
    [SerializeField] private Animator Door;
    [SerializeField] private Animator Door2;
    [SerializeField] private MissionController missionController;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private SmokeController smokeController;
    [SerializeField] private GameObject imageToHide;  // Reference to the Image component to hide
    [SerializeField] private GameObject gameComponentToShow;  // Reference to the game component to show and hide

    private string Answer = "111222";
    private bool isDoorOpened = false;

    public void Number(int number)
    {
        if (!isDoorOpened)
        {
            Ans.text += number.ToString();
        }
    }

    public void Execute()
    {
        if (!isDoorOpened && !Door.GetBool("Open"))
        {
            if (Ans.text == Answer)
            {
                Ans.text = "Unlocked";
                Door.SetBool("Open", true);
                Door2.SetBool("Open", true);
                smokeController.StopAllSmoke();
                isDoorOpened = true;
                missionController.CompleteMission();
                playerHealth.StopHealthDepletion();
                StartCoroutine(HideImageAfterDelay(2f));  // Start coroutine to hide the image
                StartCoroutine(ShowGameComponentAfterDelay(1f));  // Show the game component after 1 second
            }
            else
            {
                Ans.text = "Invalid";
                StartCoroutine(ResetDisplay());
            }
        }
    }

    IEnumerator ResetDisplay()
    {
        yield return new WaitForSeconds(2);
        if (!isDoorOpened)
        {
            Ans.text = "";
        }
    }

    IEnumerator HideImageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (imageToHide != null)
        {
            imageToHide.SetActive(false);
        }
    }

    IEnumerator ShowGameComponentAfterDelay(float delayToShow)
    {
        yield return new WaitForSeconds(delayToShow);
        if (gameComponentToShow != null)
        {
            gameComponentToShow.SetActive(true);
            StartCoroutine(HideGameComponentAfterDelay(5f));  // Hide the game component after 5 seconds
        }
    }

    IEnumerator HideGameComponentAfterDelay(float delayToHide)
    {
        yield return new WaitForSeconds(delayToHide);
        if (gameComponentToShow != null)
        {
            gameComponentToShow.SetActive(false);
        }
    }
}
