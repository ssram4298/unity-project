using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloBot : MonoBehaviour
{
    public HoloRespawnScript holoRespawnScript;

    void OnCollisionEnter(Collision collision)
    {
        holoRespawnScript.counter++;
        Debug.Log(holoRespawnScript.counter);

        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
