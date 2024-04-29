using UnityEngine;
using TMPro;
using System.Collections;

public class Keypad : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI Ans;
    [SerializeField] private Animator Door, Door2;
    [SerializeField] private GameController gameController;
    [SerializeField] private PlayerHealthController playerHealth;
    [SerializeField] private SmokeController smokeController;
    [SerializeField] private GameObject healthFX;
    [SerializeField] private AudioSource audioSource;  // Audio source component for playing sounds

    private readonly string Answer = "458";
    private bool isDoorOpened = false;

    private void Start()
    {
        // Ensure AudioSource is attached to this GameObject or manually assign it in the inspector
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void Number(int number)
    {
        if (!isDoorOpened)
        {
            Ans.text += number.ToString();
            audioSource.Play();
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
                healthFX.SetActive(true);
                isDoorOpened = true;
                playerHealth.StopHealthDepletion();
                
                gameController.CompleteMission();
                
                StartCoroutine(WaitAndStartNextMission(5f)); // Wait for 10 seconds then start next mission

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
    IEnumerator WaitAndStartNextMission(float delay)
    {
        Debug.Log("Delay Started!");
        yield return new WaitForSeconds(delay);

        Debug.Log("Keypad Called Mission2!");

        gameController.Mission2(); // Make sure this method is implemented in GameController
    }
}
