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

    // Use this in the game controller scirpt to call the soundcontroller function
    /*public void Prelude()
    {
        Debug.Log("Prelude Started!");
        StartMission();
        // Assuming the prelude audio clip is at index 0
        SoundController.Instance.PlayAudioClip(0);
        StartCoroutine(WaitForAudioClipEnd(SoundController.Instance.audioSource, Mission1));
    }
    IEnumerator WaitForAudioClipEnd(AudioSource source, Action callback)
    {
        yield return new WaitWhile(() => source.isPlaying);
        callback?.Invoke();
    }*/ 


}
