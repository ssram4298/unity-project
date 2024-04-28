using UnityEngine;
using TMPro;  // Using TextMesh Pro for UI components

public class EventDisplayManager : MonoBehaviour
{
    public GameObject hudEventMissionStart; // The entire HUD GameObject
    public TextMeshProUGUI missionNameText; // Assign your Label_MissionName Text component here in the inspector
    public TextMeshProUGUI missionCompleteText; // Assign your Label_MissionComplete Text component here in the inspector
    private float displayTime = 3.0f; // Time in seconds to display each message

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
        

        // Optionally hide the HUD after some time (commented out if persistent display is needed)
       // Invoke("HideHUD", displayTime);
      //  hideGameName();
    }

    public void DisplayStartMission(string missionName, string missionInfo)
    {
        
        hudEventMissionStart.SetActive(true); // Show the HUD again
        missionCompleteText.text = missionName; // Display the mission name
        missionNameText.text = missionInfo; // Display the mission information


        // Hide the HUD after the specified display time
        Invoke("HideHUD", displayTime);
    }

    public void DisplayEndMission(string missionName)
    {
        // Show the HUD with completion text, without hiding it automatically
        hudEventMissionStart.SetActive(true);
        missionCompleteText.text = "Mission Complete!";
        missionNameText.text = missionName;

        Invoke("HideHUD", displayTime);
    }

   private void HideHUD()
    {
        // Hide the entire HUD
        hudEventMissionStart.SetActive(false);
    }
    public void playerDied()
    {
        Debug.Log("PLayer EM");
        hudEventMissionStart.SetActive(true); // Show the HUD again
        missionCompleteText.text = "Health Reached Zero"; // Display the mission name
        missionNameText.text = "Player Died"; // Display the mission information


        // Hide the HUD after the specified display time
        Invoke("HideHUD", displayTime);
    }

}
