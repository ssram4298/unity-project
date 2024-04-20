using UnityEngine;
using TMPro;  // Using TextMesh Pro for UI components

public class EventDisplayManager : MonoBehaviour
{
    public GameObject hudEventMissionStart; // The entire HUD GameObject
    public TextMeshProUGUI missionNameText; // Assign your Label_MissionName Text component here in the inspector
    public TextMeshProUGUI missionCompleteText; // Assign your Label_MissionComplete Text component here in the inspector
    public float displayTime = 5.0f; // Time in seconds to display each message

    private void Start()
    {
        // Initially hide the entire HUD
       // hudEventMissionStart.SetActive(false);
    }

    public void DisplayGameName(string gameName)
    {
        hudEventMissionStart.SetActive(true); // Show the HUD
        missionCompleteText.text = "WELCOME TO"; // Header text
        missionNameText.text = gameName; // Display the game name
        Debug.Log("Hello");

        // Optionally hide the HUD after some time (commented out if persistent display is needed)
       // Invoke("HideHUD", displayTime);
      //  hideGameName();
    }

    public void DisplayStartMission(string missionName, string missionInfo)
    {
        Debug.Log("Hello11");
        hudEventMissionStart.SetActive(true); // Show the HUD again
        missionNameText.text = missionInfo; // Display the mission information
        missionCompleteText.text = missionName; // Display the mission name

        // Hide the HUD after the specified display time
        Invoke("HideHUD", displayTime);
    }

    public void DisplayEndMission(string missionName)
    {
        // Show the HUD with completion text, without hiding it automatically
        hudEventMissionStart.SetActive(true);
        missionCompleteText.text = "Mission Complete!";
        missionCompleteText.text = missionName;

        Invoke("HideHUD", displayTime);
    }

   private void HideHUD()
    {
        // Hide the entire HUD
        hudEventMissionStart.SetActive(false);
    }
}
