using UnityEngine;

public class DroneCircleFlight : MonoBehaviour
{
    public float radius = 1.0f; // Radius of the circle
    public float speed = 0.01f; // Speed of the flight
    private float angle = 0.0f; // Current angle of the drone on the circle

    private Vector3 centerPosition; // Center of the circle

    void Start()
    {
        centerPosition = transform.position; // Set current position as center
    }

    void Update()
    {
        angle += speed * Time.deltaTime; // Increase the angle over time
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(x, transform.position.y, z) + centerPosition; // Calculate new position
        transform.LookAt(centerPosition); // Optional: Make the drone face towards the center
    }
}
