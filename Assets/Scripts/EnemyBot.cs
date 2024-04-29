using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class EnemyBot : MonoBehaviour
{
    public Mission3Manager m3Manager;

    public AudioSource audioSource;

    public ParticleSystem[] muzzleFlash;
    public TrailRenderer tracerEffect;

    public Animator enemyAnimator;
    public Rig enemyRig;
    public float maxHealth = 100f;

    public Slider healthBarSlider;

    public Transform rayCastOrigin;
    public Transform playerTarget;  // Target the player's transform

    public float range = 100f;
    public float fireRate = 1.0f;  // Time between shots

    private Ray ray;
    private RaycastHit hitInfo;
    private bool isFiring = false;
    private float nextFireTime = 0f;
    private float currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }
        enemyRig.weight = 0;
    }

    private void Update()
    {
        if (currentHealth <= 0) return;

        if (isFiring && Time.time > nextFireTime)
        {
            StartFiring();

            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        if (playerTarget != null)
        {
            Vector3 direction = playerTarget.position - transform.position;
            direction.y = 0; // Neutralize the y component to keep the bot upright

            // Ensure the direction vector is not zero to avoid errors in LookRotation
            if (direction.magnitude > 0.1f) // Avoid normalizing a vector that's close to zero
            {
                // Create a rotation that looks along the direction vector
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // Set the bot's rotation to the target rotation immediately
                transform.rotation = targetRotation;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        RotateTowardsPlayer();

        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        // When taking damage and still alive, go into active state
        if (currentHealth > 0)
        {
            enemyRig.weight = 1;
            enemyAnimator.SetTrigger("Firing");

            if (!isFiring)
            {
                StartFiring();
            }
            
        }
        else
        {
            StopFiring();
            enemyRig.weight = 0;
            audioSource.Play();
            enemyAnimator.SetTrigger("Death");
            if (currentHealth == 0)
            {
                Invoke(nameof(Die), 2.5f);
            }
        }
    }

    public void StartFiring()
    {
        Debug.Log("Enemy Fired a bullet!");

        isFiring = true;

        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
        PerformRaycast();
        nextFireTime = Time.time + fireRate + Random.Range(0.0f, 2.0f); // Randomize firing rate
    }

    public void StopFiring()
    {
        isFiring = false;

    }

    private void Die()
    {
        gameObject.SetActive(false);
        m3Manager.IncrementCounter(); //Increment the counter in Mission3 Manager
    }

    private void PerformRaycast()
    {
        ray.origin = rayCastOrigin.position;
        ray.direction = (playerTarget.position - ray.origin).normalized; // Direct raycast towards the player

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (Physics.Raycast(ray, out hitInfo, range))
        {
            tracer.transform.position = hitInfo.point;
            Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 5.0f); // Draw a line in the Scene view

            // Check if the hit object has the tag "Player"
            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("Hit the player!");
                HandleHit();
            }
            else
            {
                Debug.Log("Hit something else: " + hitInfo.collider.name);
            }
        }
        else
        {
            Debug.Log("Missed the shot");
        }
    }

    public void HandleHit()
    {
        if (hitInfo.collider.CompareTag("Player"))
        {
            var enemyHealth = hitInfo.collider.GetComponent<PlayerHealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(5);
            }
        }
    }
}