using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource audioSource;  // Assign this in the Unity inspector
    public AudioClip[] audioClips;   // Assign audio clips for each mission in the Unity inspector

    public static SoundController Instance;

    private void Awake()
    {
        // Ensure there is only one instance of the SoundController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Optional: Make this persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(int clipIndex)
    {
        if (clipIndex < audioClips.Length && audioClips[clipIndex] != null)
        {
            audioSource.clip = audioClips[clipIndex];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip index out of range or clip is null: " + clipIndex);
        }
    }
}
