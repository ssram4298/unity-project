using UnityEngine;

public class ShootingControl : MonoBehaviour
{
    public Animator animator;           // Reference to the Animator component
    public GameObject bulletPrefab;     // The bullet prefab to shoot
    public Transform bulletSpawnPoint;  // The point from which bullets are fired
    public float fireRate = 2.0f;       // Rate at which bullets are fired (bullets per second)
    public float shootingForce = 10000f; // Increased force to propel the bullet faster and stronger
    public string shootingAnimationName = "Shooting"; // Name of the shooting animation state

    private float nextFireTime = 0.0f;  // When the next bullet can be fired

    void Update()
    {
        if (IsShootingAnimationActive() && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1.0f / fireRate; // Calculate the next fire time based on the rate
        }
    }

    private bool IsShootingAnimationActive()
    {
        if (animator == null)
            return false;

        // Check if the current state is the shooting animation
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(shootingAnimationName);
    }

    private void FireBullet()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            // Instantiate the bullet at the spawn point
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                // Apply increased force to the bullet to propel it forward
                bulletRigidbody.AddForce(bulletSpawnPoint.forward * shootingForce); // Adjusted force
            }

            Destroy(bullet, 3f); // Optionally destroy the bullet after 3 seconds to clean up
        }
    }
}
