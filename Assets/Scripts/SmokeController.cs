using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public ParticleSystem[] smokeSystems; // Array to hold all your smoke particle systems

    // Method to start all smoke effects
    public void StartAllSmoke()
    {
        foreach (var smoke in smokeSystems)
        {
            if (smoke != null && !smoke.isPlaying)
            {
                smoke.Play();
            }
        }
    }

    // Method to stop all smoke effects (if needed)
    public void StopAllSmoke()
    {
        foreach (var smoke in smokeSystems)
        {
            if (smoke != null && smoke.isPlaying)
            {
                smoke.Stop();
            }
        }
    }
}
