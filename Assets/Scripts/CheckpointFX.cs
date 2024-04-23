using UnityEngine;

public class CheckpointFX : MonoBehaviour
{
    public CheckpointManager manager; // Reference to the CheckpointManager, set this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.ReachMission2Checkpoint();
        }
    }
}
