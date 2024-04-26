using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission3Manager : MonoBehaviour
{
    public GameController gameController;
    public NotificationController notificationController;

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
            gameController.CompleteMission();
        }
    }
}
