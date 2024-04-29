using UnityEngine;

public class RectangularFlight : MonoBehaviour
{
    public Vector3[] waypoints = new Vector3[4]; // Four corners of the rectangle
    public float speed = 5.0f;                  // Speed of the car
    private int currentTarget = 0;               // Current target waypoint index
    private float threshold = 0.1f;              // Distance threshold to consider waypoint reached

    void Start()
    {
        // Initialize the starting waypoint based on the closest waypoint to the current position
        InitializeStartingWaypoint();
    }

    void Update()
    {
        if (waypoints.Length < 4) return; // Ensure there are exactly four waypoints

        // Move towards the current target waypoint
        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentTarget], speed * Time.deltaTime);

        // Check if the waypoint is reached
        if (Vector3.Distance(transform.position, waypoints[currentTarget]) < threshold)
        {
            currentTarget = (currentTarget + 1) % waypoints.Length; // Move to the next waypoint, loop back to 0 after the last one
        }

        // Optionally, make the car face the direction of movement
        if (currentTarget < waypoints.Length)
        {
            transform.LookAt(waypoints[currentTarget]);
        }
    }

    void InitializeStartingWaypoint()
    {
        float minDistance = float.MaxValue;
        int closestIndex = 0;

        // Determine the closest waypoint
        for (int i = 0; i < waypoints.Length; i++)
        {
            float distance = Vector3.Distance(transform.position, waypoints[i]);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestIndex = i;
            }
        }

        // Set the closest waypoint as the first target
        currentTarget = closestIndex;
    }
}
