using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class AdvancedEnemyBot: MonoBehaviour
{
    public ParticleSystem[] muzzleFlash;
    public TrailRenderer tracerEffect;

    public AudioSource audioSource;

    public Animator enemyAnimator;
    public Rig enemyRig;
    public float maxHealth = 100f;

    public Mission5Manager m5Manager;

    public Slider healthBarSlider;

    public Transform rayCastOrigin;
    public Transform playerTarget;  // Target the player's transform
    public float activationRange = 10f;

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

        float distanceToPlayer = Vector3.Distance(transform.position, playerTarget.position);

        if (distanceToPlayer <= activationRange && !isFiring)
        {
            StartFiring();

            RotateTowardsPlayer();
        }

        if (isFiring && Time.time > nextFireTime)
        {
            StartFiring();

            RotateTowardsPlayer();
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector3 direction = (playerTarget.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z)); // Keep the rotation in the y axis only
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }

        // When taking damage and still alive, go into active state
        if (currentHealth > 0)
        {
            if (!isFiring)
            {
                StartFiring();
            }
        }
        else
        {
            StopFiring();
            audioSource.Play();
            enemyRig.weight = 0;
            enemyAnimator.SetTrigger("Death");

            if (currentHealth == 0)
            {
                Invoke(nameof(Die), 2.5f);
            }
        }
    }

    public void StartFiring()
    {
        enemyAnimator.SetTrigger("Firing");
        enemyRig.weight = 1;

        isFiring = true;

        foreach (var particle in muzzleFlash)
        {
            particle.Emit(1);
        }
        PerformRaycast();
        RotateEnemy();
        nextFireTime = Time.time + fireRate + Random.Range(0.0f, 2.0f); // Randomize firing rate
    }

    public void RotateEnemy()
    {
        Vector3 direction = playerTarget.position - transform.position;
        direction.y = 0; // Keep the bot upright
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public void StopFiring()
    {
        isFiring = false;
    }

    private void Die()
    {
        Debug.Log("Enemy Death is being Invoked!");
        gameObject.SetActive(false);
        m5Manager.IncrementCounter();
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
            // Instantiate special effects for hitting an enemy
            //Instantiate(enemyHitEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));

            // Attempt to get the BotHealth component and call TakeDamage
            var enemyHealth = hitInfo.collider.GetComponent<PlayerHealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(5);
            }
        }
    }
}
