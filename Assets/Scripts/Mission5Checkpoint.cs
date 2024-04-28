using UnityEngine;

public class Mission5Checkpoint : MonoBehaviour
{
    public CheckpointManager manager; // Reference to the CheckpointManager, set this in the inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            manager.ReachMission5Checkpoint();
        }
    }
}
