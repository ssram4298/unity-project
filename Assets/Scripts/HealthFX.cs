using UnityEngine;

public class HealthFX : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RestoreHealth();
                // Destroy the GameObject
                Destroy(gameObject);
            }
        }
    }
}
