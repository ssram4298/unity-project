using UnityEngine;

public class TestAudio : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component missing!");
        }
        else
        {
            Debug.Log("AudioSource is present, attempting to play...");
            audioSource.Play();
        }
    }
}
