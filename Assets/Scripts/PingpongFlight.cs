using UnityEngine;

public class PingPongFlight : MonoBehaviour
{
    public Vector3 pointA;    // Start point
    public Vector3 pointB;    // End point
    public float speed = 0.1f;  // Speed at which the car moves

    private float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // Calculate the fraction of the journey based on time, speed, and distance
        float timeSinceStarted = (Time.time - startTime) * speed;
        float fractionOfJourney = Mathf.PingPong(timeSinceStarted, 1);

        // Move the car between point A and point B
        Vector3 currentPosition = Vector3.Lerp(pointA, pointB, fractionOfJourney);
        transform.position = currentPosition;

        // Determine the direction of movement and rotate the car accordingly
        if (fractionOfJourney < 0.5f) // Moving from point A to point B
        {
            transform.rotation = Quaternion.LookRotation(pointB - pointA);
        }
        else // Moving from point B to point A
        {
            transform.rotation = Quaternion.LookRotation(pointA - pointB);
        }
    }
}
