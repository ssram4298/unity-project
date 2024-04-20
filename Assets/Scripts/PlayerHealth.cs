using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;
    public TextMeshProUGUI healthText;
    public CameraShake cameraShake; // Reference to the CameraShake script

    private Gradient healthGradient;
    private Coroutine healthCoroutine; // Reference to the ongoing coroutine

    public void start1()
    {
       // currentHealth = maxHealth;
       // InitializeHealthGradient();
        //UpdateHealthUI();
        if (cameraShake == null)
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
        }
        // Start decreasing health after a 15 second delay
        healthCoroutine = StartCoroutine(DecreaseHealthOverTime(4f));
    }

    IEnumerator DecreaseHealthOverTime(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (currentHealth > 0)
        {
            yield return new WaitForSeconds(1f);
            TakeDamage(1);

            if (currentHealth <= 30)
            {
                float magnitude = 0.1f + ((30 - currentHealth) / 30.0f) * 0.4f;
                cameraShake.StartShake(0.5f, magnitude);
            }

            if (currentHealth <= 0)
            {
                yield return new WaitForSeconds(2f);
                QuitGame();
            }
        }
    }

    public void StopHealthDepletion()
    {
        if (healthCoroutine != null)
        {
            StopCoroutine(healthCoroutine);
            healthCoroutine = null;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();
    }

     public  void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercent;
        healthBarFill.color = healthGradient.Evaluate(healthPercent);
        if (healthPercent <= 0)
        {
            healthText.text = "0%";
        }
        else
        {
            healthText.text = $"{Mathf.RoundToInt(healthPercent * 100)}%";
        }
    }

   public void InitializeHealthGradient()
    {
        healthGradient = new Gradient
        {
            colorKeys = new GradientColorKey[] { new GradientColorKey(Color.green, 0.5f), new GradientColorKey(Color.red, 0f) },
            alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(1f, 1f) }
        };
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage(20); // Decrease health by 20
        }
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void RestoreHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        StopHealthDepletion();
    }
}
