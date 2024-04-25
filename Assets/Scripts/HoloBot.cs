using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloBot : MonoBehaviour
{
    public HoloRespawnScript holoRespawnScript;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            holoRespawnScript.IncrementCounter();
            gameObject.SetActive(false);
        }
    }

    public void IncrementCounter()
    {
        holoRespawnScript.IncrementCounter();
        gameObject.SetActive(false);
    }
}
