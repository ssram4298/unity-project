using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public ParticleSystem[] smokeSystems; // Array to hold all your smoke particle systems
    public AudioSource[] smokeAudio; // Array to hold audio sources corresponding to smoke particle systems

    // Method to start all smoke effects
    public void StartAllSmoke()
    {
        for (int i = 0; i < smokeSystems.Length; i++)
        {
            if (smokeSystems[i] != null && !smokeSystems[i].isPlaying)
            {
                smokeSystems[i].Play();
                if (smokeAudio[i] != null) // Ensure there is an AudioSource to play
                    smokeAudio[i].Play();
            }
        }
    }

    // Method to stop all smoke effects
    public void StopAllSmoke()
    {
        for (int i = 0; i < smokeSystems.Length; i++)
        {
            if (smokeSystems[i] != null && smokeSystems[i].isPlaying)
            {
                smokeSystems[i].Stop();
                if (smokeAudio[i] != null) // Ensure there is an AudioSource to stop
                    smokeAudio[i].Stop();
            }
        }
    }

    // Method to clear all smoke effects (removes particles instantly)
    public void ClearAllSmoke()
    {
        for (int i = 0; i < smokeSystems.Length; i++)
        {
            if (smokeSystems[i] != null && smokeSystems[i].isPlaying)
            {
                smokeSystems[i].Clear();
            }
        }
    }
}
