using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NotificationController : MonoBehaviour
{
    public GameObject hudNotificationArea;
    public TextMeshProUGUI notificationPrompt;

    public Slider notificationSlider;
    public TextMeshProUGUI notificationLabel;

    public void ActivateNotificationArea(string message)
    {
        hudNotificationArea.SetActive(true);
        notificationPrompt.text = message;
    }

    public void DeactivateNotificationArea()
    {
        hudNotificationArea.SetActive(false);
    }

    public void UpdateNotificationPrompt(string message)
    {
        notificationPrompt.text = message;
    }

    public void SetNotificaitonSlider(int maxValue, int value)
    {
        notificationSlider.maxValue = maxValue;
        notificationSlider.value = value;
    }

    public void UpdateSliderText(string message)
    {
        notificationLabel.text = message;
    }
}
