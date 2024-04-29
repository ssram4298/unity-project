using UnityEngine;

public class PingPongFlight : MonoBehaviour
{
    public Vector3 pointA;    // Start point
    public Vector3 pointB;    // End point
    private float speed = 5f;  // Speed at which the car moves

    private Vector3 currentTarget;
    private bool movingToB = true;  // Initial direction towards point B

    void Start()
    {
        currentTarget = pointB;
    }

    void Update()
    {
        // Move the car towards the current target
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);

        // Check if the car has reached the target
        if (Vector3.Distance(transform.position, currentTarget) < 0.01f)
        {
            if (movingToB)
            {
                currentTarget = pointA;  // Change target
                movingToB = false;       // Now moving towards point A
            }
            else
            {
                currentTarget = pointB;  // Change target
                movingToB = true;        // Now moving towards point B
            }
        }

        // Update rotation to always look towards the current target
        Vector3 direction = currentTarget - transform.position;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
