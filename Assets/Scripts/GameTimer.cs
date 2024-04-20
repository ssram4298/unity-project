using UnityEngine;
using TMPro;
using System.Collections;

public class GameTimer : MonoBehaviour
{
    public float timeRemaining = 55; // 60 seconds for 1 minute
    public bool timerIsRunning = false;
    //public TextMeshProUGUI timeText;
    public TextMeshProUGUI timeText1; // Assign a UI TextMeshPro element to this in the Inspector

    private void Start()
    {
        // Start with an empty text, the timer will appear after a delay
        //timeText.text = "";
        timeText1.text = "";
        //StartCoroutine(StartTimerAfterDelay(15)); // Wait for 15 seconds before starting the timer
    }

    /*private IEnumerator StartTimerAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        timerIsRunning = true;
        DisplayTime(timeRemaining);
    }*/

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timerIsRunning = false;
                //timeText.text = "You Lost"; // Update the display to show "You Lost"
                StartCoroutine(CloseGameAfterDelay(10)); // Wait for 5 seconds before closing the game
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0) { return; } // Prevent updating the time if we're showing the "You Lost" message

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        //timeText.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
        timeText1.text = string.Format("Time Remaining: {0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator CloseGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#else
        Application.Quit(); // Quit the game when built
#endif
    }

    public void StopTimer()
    {
        timerIsRunning = false; // Stop the timer
        //timeText.text = "You are Safe";
        //timeText.text = "You are";
        StopCoroutine(CloseGameAfterDelay(5)); // Ensure the close game coroutine is also stopped
    }
}
