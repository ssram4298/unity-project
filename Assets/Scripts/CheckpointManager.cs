using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public HoloRespawnScript holoRespawnScript;
    public NotificationController notificationController;
    //public GameObject hudNotificationArea;
    //public TextMeshProUGUI notificationPrompt;
    public GameObject[] Mission2Checkpoints; // Array of checkpoint GameObjects
    public GameObject[] Mission2Directions;  // Array of directional FX GameObjects
    public GameObject[] Mission3Checkpoints; // Array of checkpoint GameObjects
    public GameObject[] Mission3Directions;  // Array of directional FX GameObjects
    //private int currentCheckpointIndex = 0; // Track the current checkpoint index

    void Start()
    {
        // Initially deactivate all checkpoints and directions
        //DeactivateAllCheckpointsAndDirections();
        // Activate the first checkpoint and direction FX
        //ActivateCheckpoint(0);
    }

    /*void DeactivateAllCheckpointsAndDirections()
    {
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.SetActive(false);
        }
        foreach (GameObject direction in directions)
        {
            direction.SetActive(false);
        }
    }*/

    public void ActivateMission2Checkpoint()
    {
        foreach (GameObject checkpoint in Mission2Checkpoints)
        {
            checkpoint.SetActive(true);
        }
        foreach (GameObject direction in Mission2Directions)
        {
            direction.SetActive(true);
        }
        notificationController.UpdateSliderText("Reach the Next Checkpoint!");
    }

    public void ReachMission2Checkpoint()
    {
        // Deactivate all Mission2 Checkpoints 
        foreach (GameObject checkpoint in Mission2Checkpoints)
        {
            checkpoint.SetActive(false);
        }
        foreach (GameObject direction in Mission2Directions)
        {
            direction.SetActive(false);
        }

        //hudNotificationArea.SetActive(true);
        //notificationPrompt.text = "Kill 10 Bots!!";
        notificationController.ActivateNotificationArea("Kill 10 Bots using a Gun!");
        holoRespawnScript.StartSpawningBots(); // Call to Respawn Bots
    }

    public void ActivateMission3Checkpoint()
    {
        foreach (GameObject checkpoint in Mission3Checkpoints)
        {
            checkpoint.SetActive(true);
        }
        foreach (GameObject direction in Mission3Directions)
        {
            direction.SetActive(true);
        }
        notificationController.UpdateSliderText("Reach the Next Checkpoint!");
    }
    public void ReachMission3Checkpoint()
    {
        // Deactivate all Mission2 Checkpoints 
        foreach (GameObject checkpoint in Mission3Checkpoints)
        {
            checkpoint.SetActive(false);
        }
        foreach (GameObject direction in Mission3Directions)
        {
            direction.SetActive(false);
        }

        //hudNotificationArea.SetActive(true);
        //notificationPrompt.text = "Kill 2 Enemies!!";
        notificationController.ActivateNotificationArea("Kill 3 Enemies!");
        //function to start mission 3
    }
   

    /*public void ReachCheckpoint(int index)
    {
        // Deactivate the current checkpoint and direction FX
        if (index < checkpoints.Length && index < directions.Length)
        {
            checkpoints[index].SetActive(false);
            directions[index].SetActive(false);
        }

        // Activate the next checkpoint and direction FX
        if (index + 1 < checkpoints.Length && index + 1 < directions.Length)
        {
            ActivateCheckpoint(index + 1);
        }
    }

    private void ActivateCheckpoint(int index)
    {
        if (index < checkpoints.Length && index < directions.Length)
        {
            checkpoints[index].SetActive(true);
            directions[index].SetActive(true);
        }
    }*/
}
