using UnityEngine;
using TMPro;

public class CheckpointManager : MonoBehaviour
{
    public Mission2Manager m2Manager;
    public Mission3Manager m3Manager;
    public Mission5Manager m5Manager;

    public NotificationController notificationController;

    public GameObject[] Mission2Checkpoints; 
    public GameObject[] Mission2Directions;  

    public GameObject[] Mission3Checkpoints; 
    public GameObject[] Mission3Directions;  
    
    public GameObject[] Mission4Checkpoints; 
    public GameObject[] Mission4Directions;
    
    public GameObject[] Mission5Checkpoints;
    public GameObject[] Mission5Directions;
    //private int currentCheckpointIndex = 0; 

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

        notificationController.ActivateNotificationArea("Kill 10 Bots using a Gun!");
        m2Manager.StartSpawningBots(); // Call to Respawn Bots
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

        notificationController.ActivateNotificationArea("Kill 3 Enemies!");

        m3Manager.SetupCounter();
    }

    public void ActivateMission4Checkpoint()
    {
        foreach (GameObject checkpoint in Mission4Checkpoints)
        {
            checkpoint.SetActive(true);
        }
        foreach (GameObject direction in Mission4Directions)
        {
            direction.SetActive(true);
        }

        notificationController.UpdateSliderText("Reach the Next Checkpoint!");
    }

    public void ReachMission4Checkpoint()
    {
        foreach (GameObject checkpoint in Mission4Checkpoints)
        {
            checkpoint.SetActive(false);
        }
        foreach (GameObject direction in Mission4Directions)
        {
            direction.SetActive(false);
        }

        notificationController.UpdateSliderText("Talk With the Secret Agent!");
    }

    public void ActivateMission5Checkpoint()
    {
        foreach (GameObject checkpoint in Mission5Checkpoints)
        {
            checkpoint.SetActive(true);
        }
        foreach (GameObject direction in Mission5Directions)
        {
            direction.SetActive(true);
        }

        notificationController.UpdateSliderText("Reach the Next Checkpoint!");
    }

    public void ReachMission5Checkpoint()
    {
        foreach (GameObject checkpoint in Mission5Checkpoints)
        {
            checkpoint.SetActive(false);
        }
        foreach (GameObject direction in Mission5Directions)
        {
            direction.SetActive(false);
        }

        notificationController.UpdateSliderText("Defeat the Enemies!");

        m5Manager.StartMission5();
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
