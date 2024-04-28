using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3Manager : MonoBehaviour
{
    public GameController gameController;
    public NotificationController notificationController;

    public GameObject[] healthFX;

    private int counter = 0;
    private int enemiesToDefeat = 3;

    public void SetupCounter()
    {
        notificationController.SetNotificaitonSlider(enemiesToDefeat, counter);
        notificationController.UpdateSliderText(counter + "/" + enemiesToDefeat);
    }

    public void IncrementCounter()
    {
        counter++;
        notificationController.SetNotificaitonSlider(enemiesToDefeat, counter);
        notificationController.UpdateSliderText(counter + "/" + enemiesToDefeat);

        if (counter >= 3)
        {
            healthFX[counter].SetActive(true);
            notificationController.DeactivateNotificationArea();
            gameController.CompleteMission();
            gameController.Mission4();
        }
    }
}
