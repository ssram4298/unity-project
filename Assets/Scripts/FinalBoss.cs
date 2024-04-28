using UnityEngine;
using System.Collections;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using TMPro;

public class BossController : MonoBehaviour
{
    public Animator BossAnimator;

    public Transform attackRayCastOrigin;
    public Transform bigAttackRayCastOrigin;
    public Transform playerTarget;

    public Rig bossRig;

    public Transform player; // Assign player's Transform in the Inspector
    public LayerMask playerLayer; // Set up a layer mask for the player in the Inspector

    public ParticleSystem attackFX;
    public ParticleSystem bigAttackFX; // Assign this FX GameObject in the Inspector

    public Slider BossHealthSlider;
    public TextMeshProUGUI HealthLabel;

    private Ray ray;
    private RaycastHit hitInfo;

    private float maxHealth = 1000f;
    private float currentHealth;
    private float attackTimer = 5f;

    private Coroutine bigAttackRoutine = null;


    // Start is called before the first frame update
    void Start()
    {
        BossAnimator.Play("Standing Idle");
        currentHealth = maxHealth;
        if (BossHealthSlider != null)
        {
            BossHealthSlider.maxValue = maxHealth;
            BossHealthSlider.value = currentHealth;

            HealthLabel.text = currentHealth + "/" + maxHealth + " HP";
        }
        bigAttackFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            if (currentHealth > maxHealth * 0.5f)
            {
                // Boss is above 50% health
                PerformRaycastAttack(5); // Damage is 10
                attackTimer = 4f; // Reset timer to 4 seconds
            }
            else
            {
                // Boss is below 50% health
                Debug.Log("Big Attack Called");
                bigAttackRoutine = StartCoroutine(ChargeAndPerformBigAttack());
                attackTimer = 10f; // Reset timer to 10 seconds
            }
        }
    }

    private void LateUpdate()
    {
        LookAtPlayer();
    }

    void LookAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5); // Adjust 5 to change rotation speed
    }


    void PerformRaycastAttack(int damage)
    {
        ray.origin = attackRayCastOrigin.position;
        ray.direction = (playerTarget.position - ray.origin).normalized; // Direct raycast towards the player

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            Debug.DrawLine(ray.origin, hitInfo.point, Color.blue, 5.0f); // Draw a line in the Scene view

            // Check if the hit object has the tag "Player"
            if (hitInfo.collider.CompareTag("Player"))
            {
                attackFX.Play();
                Debug.Log("Hit the player!");
                HandleHit(damage);
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

    public void HandleHit(int damage)
    {
        if (hitInfo.collider.CompareTag("Player"))
        {
            var enemyHealth = hitInfo.collider.GetComponent<PlayerHealthController>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
            }
        }
    }

    IEnumerator ChargeAndPerformBigAttack()
    {
        bigAttackFX.Play();
        yield return new WaitForSeconds(3);
        BossAnimator.Play("Attack");
        yield return new WaitForSeconds(2);

        ray.origin = attackRayCastOrigin.position;
        ray.direction = (playerTarget.position - ray.origin).normalized; 

        if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity))
        {
            if (hitInfo.collider.CompareTag("Player"))
            {
                Debug.Log("Hit the player!");
                HandleHit(25);
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

        yield return new WaitForSeconds(2.5f);
        BossAnimator.Play("Standing Idle");

        bigAttackRoutine = null;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if(BossHealthSlider != null )
        {
            BossHealthSlider.value = currentHealth;
            HealthLabel.text = currentHealth + "/" + maxHealth + " HP";
        }

        if (currentHealth <= 0)
        {
            if (bigAttackRoutine != null)
            {
                StopCoroutine(bigAttackRoutine);
                bigAttackRoutine = null;
                bigAttackFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Ensure to stop the particle effects of the big attack
            }

            StopAllCoroutines();
            bigAttackFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Stop and clear big attack effects

            bossRig.weight = 0;
            
            BossAnimator.Play("Dying");
            Invoke(nameof(Die), 4.5f);
        }
    }

    void Die()
    {
        Debug.Log("Boss is dead!");
        
        // Handle boss death here, like disabling the boss game object, playing death animations, etc.
        gameObject.SetActive(false);
    }
}
