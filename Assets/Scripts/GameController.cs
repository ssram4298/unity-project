using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    public EventDisplayManager eventDisplayManager;
    public GameObject pressAnyButtonPrompt;
    public PlayerHealthController playerHealth;
    public SmokeController smokeController;
    public GameObject HudCallEvent;
    public GameObject KeyPadTrigger;
    public AudioSource audioSource; // Assign this in the Unity inspector
    public AudioSource audioSource2;

    private bool isWaitingForInput = true; // Waiting for any input, not just displaying game name
    private bool gameStarted = false; // Flag to ensure the game start logic only runs once

    private string gameName = "Cyberpunk!";
    private int currentMissionIndex = 0;

    private string[] missionNames = { "Prelude", "Mission 1", "Mission 2", "Mission 3" };
    private string[] missionDescriptions = { "The Call.", "Escape the Room.", "The Training Arc.", "The Revenge." };

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Mission names count: " + missionNames.Length);
        foreach (var name in missionNames)
        {
            Debug.Log("Mission name: " + name);
        }
        smokeController.StopAllSmoke();
        smokeController.ClearAllSmoke();
        eventDisplayManager.DisplayGameName(gameName);  // Display the game name at the start
        pressAnyButtonPrompt.SetActive(true);        // Display the 'Press any button to continue' prompt
    }

    void Update()
    {
        // Check for any input if we are waiting for player input
        if (!gameStarted && isWaitingForInput && Input.GetButtonDown("Fire1"))
        {
            Debug.Log("User Pressed A Button");
            eventDisplayManager.Invoke("HideHUD", 0f);
            gameStarted = true;
            isWaitingForInput = false; // Stop checking for input
            pressAnyButtonPrompt.SetActive(false); // Hide the prompt
            StartCoroutine(Wait(2f, StartGame)); // Wait for 2 seconds then start the game
        }
    }
    IEnumerator Wait(float delay, System.Action callback)
    {
        yield return new WaitForSeconds(delay);
        callback?.Invoke();
    }

    private void StartGame()
    {
        Debug.Log("Game Started!");
        //Prelude();// Start the first mission
        Mission2();
    }

    public void StartMission()
    {
        Debug.Log("Start Mission Called! "+missionNames.Length);
        if (currentMissionIndex < missionNames.Length)
        {
            string missionName = missionNames[currentMissionIndex];
            string missionDescription = missionDescriptions[currentMissionIndex];
            eventDisplayManager.DisplayStartMission(missionName, missionDescription);
            currentMissionIndex++; // Increment the mission index
            
            Debug.Log("CurrentMissionIndex = "+currentMissionIndex);
        }
        else
        {
            Debug.Log("No more missions to start.");
        }
    }

    public void Prelude()
    {
        Debug.Log("Prelude Started!");
        StartMission();
        if (audioSource.clip != null)
        {
            HudCallEvent.SetActive(true);
            audioSource.Play();
            audioSource2.Play();

            /*StartCoroutine(Wait(audioSource.clip.length, () =>
            {
                HudCallEvent.SetActive(false);
                smokeController.StartAllSmoke();
                playerHealth.StartDecreasing();
                KeyPadTrigger.SetActive(true);
                StartMission();
            })); */
            StartCoroutine(Wait(audioSource.clip.length, Mission1));
        }
    }

    public void Mission1()
    {
        Debug.Log("Mission 1 Started!");

        HudCallEvent.SetActive(false);
        smokeController.StartAllSmoke();
        playerHealth.StartDecreasing();
        KeyPadTrigger.SetActive(true);
        //currentMissionIndex--;
        Debug.Log("Mission 1 called Start Mission!");
        StartMission();
    }
    public void Mission2()
    {
        Debug.Log("Mission 2 Started!");
        Debug.Log("Mission 2 called Start Mission!");
        currentMissionIndex = 2;
        StartMission();
    }

    // Call this when a mission is completed
    public void CompleteMission()
    {
        eventDisplayManager.DisplayEndMission(missionDescriptions[currentMissionIndex-1]); // Make sure this function exists in your EventDisplayManager
        
        // Optionally, start the next mission or handle the end of the game
    }
}
