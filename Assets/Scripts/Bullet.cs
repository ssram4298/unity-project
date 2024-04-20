using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 100f;
    public float maxDistance = 100f;

    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        Vector3 nextPosition = transform.position + transform.forward * speed * Time.deltaTime;
        RaycastHit hit;
        if (Physics.Raycast(lastPosition, (nextPosition - lastPosition).normalized, out hit, (nextPosition - lastPosition).magnitude))
        {
            // Hit detected
            Debug.Log("Hit: " + hit.collider.name);
            // Optional: Apply damage, effects, etc.
        }

        lastPosition = transform.position = nextPosition;
    }
}
