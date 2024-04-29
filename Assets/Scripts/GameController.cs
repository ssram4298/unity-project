using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    public EventDisplayManager eventDisplayManager;
    public GameObject pressAnyButtonPrompt;
    public PlayerHealthController playerHealth;
    public SmokeController smokeController;
    public CheckpointManager checkpointManager;
    public SoundController callController;

    public GameObject HudCallEvent;
    public GameObject KeyPad;
    public AudioSource audioSource; // Assign this in the Unity inspector
    public AudioSource audioSource2;

    private bool isWaitingForInput = true; // Waiting for any input, not just displaying game name
    private bool gameStarted = false; // Flag to ensure the game start logic only runs once

    private string gameName = "Operation Hunt!";
    private int currentMissionIndex = 0;

    private string[] missionNames = { "Prelude", "Mission 1", "Mission 2", "Mission 3", "Mission 4", "Mission 5" };
    private string[] missionDescriptions = { "The Call.", "Breathless Escape!", "The Training Arc.", "Silent Strike!","Echoes of Espionage!", "The Final Gambit!" };

    // Start is called before the first frame update
    void Start()
    {
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
        Prelude();// Start the first mission
        //
        //Mission2();
        //Mission3();
        //Mission5();
    }

    public void StartMission()
    {
        Debug.Log("Start Mission Called! "+missionNames.Length);
        if (currentMissionIndex < missionNames.Length)
        {
            string missionName = missionNames[currentMissionIndex];
            string missionDescription = missionDescriptions[currentMissionIndex];
            SoundController.Instance.PlayAudioClip(0, false);
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
        HudCallEvent.SetActive(true);
        /*if (audioSource.clip != null)
        {
            HudCallEvent.SetActive(true);
            audioSource.Play();
            audioSource2.Play();

            StartCoroutine(Wait(audioSource.clip.length, () =>
            {
                HudCallEvent.SetActive(false);
                smokeController.StartAllSmoke();
                playerHealth.StartDecreasing();
                KeyPadTrigger.SetActive(true);
                StartMission();
            }));
            StartCoroutine(Wait(audioSource.clip.length, Mission1));
        }*/
        SoundController.Instance.PlayAudioClip(2, true);
        StartCoroutine(WaitForAudioClipEnd(SoundController.Instance.primaryAudioSource, Mission1));
    }
    
    IEnumerator WaitForAudioClipEnd(AudioSource source, Action callback)
    {
        yield return new WaitWhile(() => source.isPlaying);
        callback?.Invoke();
    } 

    public void Mission1()
    {
        Debug.Log("Mission 1 Started!");

        HudCallEvent.SetActive(false);
        smokeController.StartAllSmoke();
        playerHealth.StartDecreasing();
        KeyPad.SetActive(true);
        Debug.Log("Mission 1 called Start Mission!");
        StartMission();
    }

    public void Mission2()
    {
        KeyPad.SetActive(false);
        Debug.Log("Mission 2 Started!");

        currentMissionIndex = 2; // Remove this after development or dont it doesnt matter
        StartMission();
        checkpointManager.ActivateMission2Checkpoint();
    }

    public void Mission3()
    {
        Debug.Log("Mission 3 Started!");

        HudCallEvent.SetActive(true);
        SoundController.Instance.PlayAudioClip(3, true);
        
        checkpointManager.ActivateMission3Checkpoint();
        
        StartCoroutine(WaitForAudioClipEnd(SoundController.Instance.primaryAudioSource, () =>
        {
            HudCallEvent.SetActive(false);

            currentMissionIndex = 3;
            StartMission();
        }));
        
    }

    public void Mission4()
    {
        Debug.Log("Mission 4 Started!");

        HudCallEvent.SetActive(true);
        SoundController.Instance.PlayAudioClip(4, true);
        //wait for audioclip to end and call checkpointmanager.ActivateMission4checkpoint
        StartCoroutine(WaitForAudioClipEnd(SoundController.Instance.primaryAudioSource, () =>
        {
            HudCallEvent.SetActive(false);

            currentMissionIndex = 4; // Remove this after development or dont it doesnt matter
            StartMission();
            checkpointManager.ActivateMission4Checkpoint();
        }));
    }

    public void Mission5()
    {
        Debug.Log("Mission 5 Started");
        currentMissionIndex = 5;
        checkpointManager.ActivateMission5Checkpoint();
    }

    // Call this when a mission is completed
    public void CompleteMission()
    {
        SoundController.Instance.PlayAudioClip(1, false);
        Debug.Log("Complete Mission Called for Mission " + (currentMissionIndex-1));
        eventDisplayManager.DisplayEndMission(missionDescriptions[currentMissionIndex-1]); // Make sure this function exists in your EventDisplayManager
        
        // Optionally, start the next mission or handle the end of the game
    }
    public void playerHealthZero()
    {
        eventDisplayManager.playerDied();
    }
}
