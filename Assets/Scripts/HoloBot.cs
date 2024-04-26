using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloBot : MonoBehaviour
{
    public Mission2Manager m2Manager;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            m2Manager.IncrementCounter();
            gameObject.SetActive(false);
        }
    }

    public void IncrementCounter()
    {
        m2Manager.IncrementCounter();
        gameObject.SetActive(false);
    }
}
