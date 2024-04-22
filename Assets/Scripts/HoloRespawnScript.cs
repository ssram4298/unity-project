using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoloRespawnScript : MonoBehaviour
{
    public GameController gameController;
    public NotificationController notificationController;
    public GameObject wallToRemove;

    public List<GameObject> allObjects; // List to store all objects that can be activated/deactivated

    //public Slider botCountSlider;
    //public TextMeshProUGUI botCountLabel;
    public int botsToDefeat = 10;


    public float appearanceInterval = 2f; // Interval in seconds to activate objects
    public float displayTime = 5f; // Duration in seconds each object stays active
    private int counter = 0;

    public void StartSpawningBots()
    {
        /*if(botCountSlider != null)
        {
            botCountSlider.maxValue = botsToDefeat;
            botCountSlider.value = counter;
            botCountLabel.text = counter + "/" + botsToDefeat; 
        }*/
        notificationController.SetNotificaitonSlider(botsToDefeat, counter);
        notificationController.UpdateSliderText(counter + "/" + botsToDefeat);
        // Start the periodic activation and deactivation routine
        StartCoroutine(SequentialActivationRoutine());
    }

    public void IncrementCounter()
    {
        counter++;
        //botCountSlider.value = counter;
        //botCountLabel.text = counter + "/" + botsToDefeat;
        notificationController.SetNotificaitonSlider(botsToDefeat, counter);
        notificationController.UpdateSliderText(counter + "/" + botsToDefeat);
    }

    IEnumerator SequentialActivationRoutine()
    {
        // Continuously cycle through all objects
        while (true)
        {
            foreach (var obj in allObjects)
            {
                if (counter >= 10)
                {
                    // call missioncomplete
                    gameController.CompleteMission();
                    notificationController.DeactivateNotificationArea();
                    notificationController.UpdateSliderText("");
                    yield return new WaitForSeconds(5f);
                    wallToRemove.SetActive(false);
                    gameController.Mission3();
                    break;
                }
                // Activate the object
                obj.SetActive(true);

                // Wait for the display time before deactivating the object
                yield return new WaitForSeconds(displayTime);
                obj.SetActive(false);

                // Wait for the appearance interval before activating the next object
                yield return new WaitForSeconds(appearanceInterval);
            }
            break;
        }
    }
}
