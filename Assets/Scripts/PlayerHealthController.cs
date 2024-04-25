using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class PlayerHealthController: MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;
    public TextMeshProUGUI healthText;

    private Gradient healthGradient;
    private Coroutine healthCoroutine; // Reference to the ongoing coroutine

    public void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthGradient();
        UpdateHealthUI();
    }

    public void StartDecreasing()
    {
        // Start decreasing health after a 2 second delay
        healthCoroutine = StartCoroutine(DecreaseHealthOverTime(2f));
    }

    IEnumerator DecreaseHealthOverTime(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        while (currentHealth > 0)
        {
            float delay = currentHealth >= 70 ? 2f : 1f;
            yield return new WaitForSeconds(delay);
            TakeDamage(1);
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
        Debug.Log("Player took " + damage + " Damage!!");
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            //yield return new WaitForSeconds(2f);
            QuitGame();
        }
    }

     public  void UpdateHealthUI()
    {
        float healthPercent = (float)currentHealth / maxHealth;
        healthBarFill.fillAmount = healthPercent;
        healthBarFill.color = healthGradient.Evaluate(healthPercent);
        healthText.text = currentHealth <= 0 ? "0%" : $"{Mathf.RoundToInt(healthPercent * 100)}%";
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
            TakeDamage(10); // Decrease health by 10
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
    }
}
