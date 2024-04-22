using UnityEngine;

public class Mission3CheckpointFX : MonoBehaviour
{
    //public int checkpointIndex; // Index of this checkpoint, set this in the inspector
    public CheckpointManager checkpointManager; // Reference to the CheckpointManager, set this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpointManager.ReachMission3Checkpoint();
        }
    }
}
