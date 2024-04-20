using UnityEngine;
using UnityEngine.UI;

public class BotHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider healthBarSlider; // Assign the health bar slider component
    public Animator animator; // Assign the Animator component
    public GameObject secondaryObject; // Assign the secondary object that should disappear after delay
    public Transform playerTransform; // Assign the player's transform here

    private const string startAnimation = "Idle";
    private const string activeAnimation = "Shooting";
    private const string zeroHealthAnimation = "Death";

    private int currentHealth;
    private bool isPlayerNear = false;
    private bool isHealthLow = false;
    private bool shouldRotate = true;  // Flag to control rotation based on animation state

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBarSlider != null)
        {
            healthBarSlider.maxValue = maxHealth;
            healthBarSlider.value = currentHealth;
        }
        animator.Play(startAnimation);
    }

    void Update()
    {
        if (playerTransform != null)
        {
            Vector3 lookDirection = playerTransform.position - transform.position;
            lookDirection.y = 0; // Keep the bot upright, focusing only on turning around the y-axis
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // If shouldRotate is true, rotate normally. If false, apply a fixed offset rotation.
            if (shouldRotate)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5);
            }
            else
            {
                // Apply a -45 degrees rotation on the Y-axis while maintaining the target direction
                Quaternion offsetRotation = Quaternion.Euler(0, 40, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation * offsetRotation, Time.deltaTime * 5);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth < 100 && !isHealthLow)
        {
            isHealthLow = true;
            animator.Play(activeAnimation);
            shouldRotate = false;  // Change rotation behavior when Shooting animation starts
        }
        else if (currentHealth >= 100)
        {
            isHealthLow = false;
            if (!isPlayerNear)
            {
                animator.Play(startAnimation);
                shouldRotate = true;  // Resume normal rotation
            }
        }

        if (currentHealth <= 0)
        {
            animator.Play(zeroHealthAnimation);
            Invoke(nameof(Die), 3.0f); // Delay the bot's disappearance to allow for death animation
            if (secondaryObject != null)
            {
                Invoke(nameof(HideSecondaryObject), 1.5f); // Delay the secondary object's disappearance
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBarSlider != null)
        {
            healthBarSlider.value = currentHealth;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            animator.Play(activeAnimation);
            shouldRotate = false;  // Change rotation behavior
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }

    private void HideSecondaryObject()
    {
        if (secondaryObject != null)
        {
            secondaryObject.SetActive(false);
        }
    }
}