using UnityEngine;

public class FlyingCar : MonoBehaviour
{
    public float radius = 5f;      // Radius of the circle
    public float speed = 1f;       // Angular speed (radians per second)
    private float angle = 0f;      // Current angle of the car on the circle

    private Vector3 center;        // Center of the circle

    void Start()
    {
        // Set the center of the circle based on the car's initial position offset by the radius
        // Assume we start the motion going towards positive X initially, so offset center to the left or right accordingly
        center = transform.position + new Vector3(0, 0, radius);
    }

    void Update()
    {
        // Calculate the new angle using the speed and time
        angle += speed * Time.deltaTime;

        // Calculate new position using the center, radius, and angle
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        Vector3 newPosition = new Vector3(center.x + x, transform.position.y, center.z + z);

        // Update the car's position
        transform.position = newPosition;

        // Make the car face the direction of movement by looking at the next point along the circle
        Vector3 nextPosition = new Vector3(center.x + Mathf.Cos(angle + 0.1f) * radius, center.y, center.z + Mathf.Sin(angle + 0.1f) * radius);
        transform.LookAt(nextPosition);
    }
}
