using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    public void QuitGame()
    {
        // If we are running in the Unity editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // If we are running in a standalone build of the game
        Application.Quit();
#endif
    }
}
