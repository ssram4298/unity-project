using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public GameObject[] checkpoints; // Array of checkpoint GameObjects
    public GameObject[] directions;  // Array of directional FX GameObjects
    private int currentCheckpointIndex = 0; // Track the current checkpoint index

    void Start()
    {
        // Initially deactivate all checkpoints and directions
        DeactivateAllCheckpointsAndDirections();
        // Activate the first checkpoint and direction FX
        ActivateCheckpoint(0);
    }

    void DeactivateAllCheckpointsAndDirections()
    {
        foreach (GameObject checkpoint in checkpoints)
        {
            checkpoint.SetActive(false);
        }
        foreach (GameObject direction in directions)
        {
            direction.SetActive(false);
        }
    }

    public void ReachCheckpoint(int index)
    {
        // Deactivate the current checkpoint and direction FX
        if (index < checkpoints.Length && index < directions.Length)
        {
            checkpoints[index].SetActive(false);
            directions[index].SetActive(false);
        }

        // Activate the next checkpoint and direction FX
        if (index + 1 < checkpoints.Length && index + 1 < directions.Length)
        {
            ActivateCheckpoint(index + 1);
        }
    }

    private void ActivateCheckpoint(int index)
    {
        if (index < checkpoints.Length && index < directions.Length)
        {
            checkpoints[index].SetActive(true);
            directions[index].SetActive(true);
        }
    }
}
