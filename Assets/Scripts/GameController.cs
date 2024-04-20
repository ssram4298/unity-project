using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public EventDisplayManager eventDisplayManager;
    public GameObject pressAnyButtonPrompt;
    public PlayerHealthController playerHealth;
    public SmokeController smokeController;
    public GameObject hudCallEvent;
    public AudioSource audioSource; // Assign this in the Unity inspector
    public AudioSource audioSource2;

    private bool isWaitingForInput = true; // Waiting for any input, not just displaying game name
    private bool gameStarted = false; // Flag to ensure the game start logic only runs once

    public string gameName = "Your Game Name";
    private int currentMissionIndex = 0;

    public string[] missionNames = {
        "Prelude",
        "Mission 1",
        // Add more mission names here
    };

    public string[] missionDescriptions = {
        "The Call.",
        "Escape the Room.",
        // Add more mission descriptions here
    };

    // Start is called before the first frame update
    void Start()
    {
        smokeController.StopAllSmoke();
        smokeController.ClearAllSmoke();
        eventDisplayManager.DisplayGameName(gameName);  // Display the game name at the start
        pressAnyButtonPrompt.SetActive(true);        // Display the 'Press any button to continue' prompt
        hudCallEvent.SetActive(false);
    }

    void Update()
    {
        // Check for any input if we are waiting for player input
        if (!gameStarted && isWaitingForInput && Input.anyKeyDown)
        {
            Debug.Log("User Pressed A Button");
            eventDisplayManager.Invoke("HideHUD", 0f);
            gameStarted = true;
            isWaitingForInput = false; // Stop checking for input
            pressAnyButtonPrompt.SetActive(false); // Hide the prompt
            StartCoroutine(WaitAndStartFirstMission()); // Start the coroutine to wait for 10 seconds
        }
    }

    private IEnumerator WaitAndStartFirstMission()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);
        StartGame();
    }

    private void StartGame()
    {
        // Start the first mission
        Debug.Log("Game Started!");
        Prelude();
    }

    public void StartMission()
    {
        if (currentMissionIndex < missionNames.Length)
        {
            Debug.Log("Displayed mission");

            string missionName = missionNames[currentMissionIndex];
            string missionDescription = missionDescriptions[currentMissionIndex];
            eventDisplayManager.DisplayStartMission(missionName, missionDescription);
            currentMissionIndex++; // Increment the mission index

            Debug.Log(currentMissionIndex);
        }
        else
        {
            Debug.Log("No more missions to start.");
        }
    }

    public void Prelude()
    {
        StartMission();
        if (audioSource.clip != null)
        {
            Debug.Log("Started playing audio!");
            hudCallEvent.SetActive(true);
            audioSource.Play();
            audioSource2.Play();
            StartCoroutine(WaitForAudioToEnd()); 
        }
    }
    private IEnumerator WaitForAudioToEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        hudCallEvent.SetActive(false);
        smokeController.StartAllSmoke();
        playerHealth.StartDecreasing();
        StartMission();
    }

    // Call this when a mission is completed
    public void CompleteMission()
    {
        eventDisplayManager.DisplayEndMission(missionNames[currentMissionIndex-1]); // Make sure this function exists in your EventDisplayManager
        
        // Optionally, start the next mission or handle the end of the game
    }
}
