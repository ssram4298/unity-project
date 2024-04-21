using UnityEngine;

public class CheckpointFX : MonoBehaviour
{
    //public int checkpointIndex; // Index of this checkpoint, set this in the inspector
    public CheckpointManager manager; // Reference to the CheckpointManager, set this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.ReachMission2Checkpoint();
        }
    }
}
