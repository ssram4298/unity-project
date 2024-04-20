using UnityEngine;

public class HealthFX : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealthController playerHealth = other.GetComponent<PlayerHealthController>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth();
                // Destroy the GameObject
                Destroy(gameObject);
            }
        }
    }
}
