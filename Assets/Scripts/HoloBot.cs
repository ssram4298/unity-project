using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloBot : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
    }
}
