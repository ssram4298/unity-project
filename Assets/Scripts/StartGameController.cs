using UnityEngine;

public class StartGameController : MonoBehaviour
{
    public GameObject startPrompt; // UI or object to instruct player to press button
    private bool gameStarted = false;

    void Start()
    {
        // Initial setup that can safely happen before the game starts
        startPrompt.SetActive(true);
        InitializeSettings();
    }

    void Update()
    {
        // Check for the specific start button press
        if (!gameStarted && Input.GetButtonDown("Fire1")) // Assuming Fire1 is mapped to your desired start button
        {
            StartGame();
        }
    }

    private void InitializeSettings()
    {
        // Perform any necessary initial setup that doesn't depend on the game being "started"
        Debug.Log("Settings Initialized - Waiting for player to start the game.");
    }

    private void StartGame()
    {
        // Ensure this part runs only once
        if (gameStarted) return;

        // Hide the start prompt and mark the game as started
        startPrompt.SetActive(false);
        gameStarted = true;

        // Perform tasks that should only execute after the game starts
        LoadGameLevels();
        InitializeGameComponents();
        Debug.Log("Game Started!");
    }

    private void LoadGameLevels()
    {
        // Load levels or perform other heavy tasks
        Debug.Log("Levels Loaded");
    }

    private void InitializeGameComponents()
    {
        // Initialize gameplay components or managers
        Debug.Log("Game Components Initialized");
    }
}
