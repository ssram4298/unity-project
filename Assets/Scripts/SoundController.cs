using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource primaryAudioSource;  // For important sounds, assign in Unity inspector
    public AudioSource secondaryAudioSource;  // For less important sounds, assign in Unity inspector
    public AudioClip[] audioClips;   // Assign audio clips for each mission in the Unity inspector

    public static SoundController Instance;

    private void Awake()
    {
        // Ensure there is only one instance of the SoundController
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make this persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayAudioClip(int clipIndex, bool isImportant = false)
    {
        AudioSource targetSource = isImportant ? primaryAudioSource : secondaryAudioSource;
        if (clipIndex < audioClips.Length && audioClips[clipIndex] != null)
        {
            targetSource.clip = audioClips[clipIndex];
            targetSource.Play();
        }
        else
        {
            Debug.LogError("Audio clip index out of range or clip is null: " + clipIndex);
        }
    }
}
