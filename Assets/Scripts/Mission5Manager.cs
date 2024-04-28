using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission5Manager : MonoBehaviour
{
    public GameController gameController;
    public NotificationController notificationController;

    public GameObject BlastFX;
    public GameObject BossEnemy;
    public GameObject ExtraProps;
    public GameObject FireWorks;

    public GameObject[] healthFX;

    private int counter = 0;
    private int enemiesToDefeat = 6;

    public void StartMission5()
    {
        gameController.StartMission();
        notificationController.ActivateNotificationArea("Defeat all the enemies!");
        notificationController.SetNotificaitonSlider(enemiesToDefeat, counter);
        notificationController.UpdateSliderText(counter + "/" + enemiesToDefeat);
    }

    public void IncrementCounter()
    {
        counter++;
        healthFX[counter].SetActive(true);
        notificationController.SetNotificaitonSlider(enemiesToDefeat, counter);
        notificationController.UpdateSliderText(counter + "/" + enemiesToDefeat);

        if (counter >= 6)
        {
            BossBattle();
        }
    }

    public void BossBattle()
    {
        ExtraProps.SetActive(false);
        BlastFX.SetActive(true);
        StartCoroutine(ShowBossAfterDelay(2.0f)); // Wait for 2 seconds before showing boss

        healthFX[counter + 1].SetActive(true);
        healthFX[counter + 2].SetActive(true);
    }

    IEnumerator ShowBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        BossEnemy.SetActive(true);

        // After boss appears, you might want to initiate combat mechanics or effects
        StartBossCombatMechanics();
    }

    void StartBossCombatMechanics()
    {
        // Example: Enable boss abilities, change music, etc.
        Debug.Log("Boss combat starts now!");
        // You might also want to update the notification area with new instructions
        notificationController.ActivateNotificationArea("Defeat the Boss!");
    }

    public void EndBossBattle()
    {
        Debug.Log("Boss defeated!");
        FireWorks.SetActive(true);
        // Post-battle effects or transitions can be handled here
        notificationController.ActivateNotificationArea("Boss defeated! Mission Complete.");
        gameController.CompleteMission(); // Assuming there's a method to complete the mission
    }

}
