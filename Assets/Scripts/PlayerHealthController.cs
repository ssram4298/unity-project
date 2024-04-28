using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.PostProcessing;

public class PlayerHealthController: MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;
    public TextMeshProUGUI healthText;

    public GameController gameController;

    public PostProcessVolume volume;

    private Vignette vignette;

    private Gradient healthGradient;
    private Coroutine healthCoroutine; // Reference to the ongoing coroutine
    



    public void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthGradient();
        UpdateHealthUI();

        if (volume.profile.TryGetSettings(out Vignette vignette))
        {
            this.vignette = vignette;
        }
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

        StartCoroutine(ShowDamageEffect());

        if (currentHealth <= 0)
        {
            Debug.Log("PLayer Health Reached zero");
            gameController.playerHealthZero();
            Invoke("QuitGame", 6f);
            //QuitGame();
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
        if(currentHealth <= 70)
        {
            currentHealth += 30;
        }
        else
        {
            currentHealth = maxHealth;
        }
        UpdateHealthUI();
    }

    IEnumerator ShowDamageEffect()
    {
        // Increase the vignette intensity
        vignette.intensity.value = 0.5f;  // Adjust the value as needed for visibility
        vignette.color.value = Color.red;  // Set the vignette color to red
        yield return new WaitForSeconds(0.5f);  // Duration of the effect

        // Reset the vignette intensity
        vignette.intensity.value = 0f;
    }
}
