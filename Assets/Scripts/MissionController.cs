using UnityEngine;
using TMPro;

public class MissionController : MonoBehaviour
{
    public TextMeshProUGUI missionText; // Assign this in the inspector
    private int currentMissionIndex = 0;
    private string[] missions = {
        "Unlock the keypad",
        "Kill The Bots",
        // Add more missions as needed
    };

    void Start()
    {
        // Start the first mission
        UpdateMissionText();
    }

    public void UpdateMissionText()
    {
        // Update the UI text to display the current mission
        missionText.text =  missions[currentMissionIndex];
    }

    public void CompleteMission()
    {
        // Call this method when a mission is completed

        // Increment the mission index to advance to the next mission
        currentMissionIndex++;

        // Check if we've run out of missions
        if (currentMissionIndex >= missions.Length)
        {
            // Handle the end of all missions, for example:
            missionText.text = "All missions completed!";
            return;
        }

        // Update the mission text for the new mission
        UpdateMissionText();
    }
}
