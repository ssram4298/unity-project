using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class PlayerHealthController: MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public Image healthBarFill;
    public TextMeshProUGUI healthText;

    public GameController gameController;
    public SoundController soundController;
   
    //public PostProcessVolume volume;

    public CanvasGroup damageCanvasGroup;
    public CanvasGroup healCanvasGroup;

    //private Vignette vignette;
    private float fadeDuration = 0.5f;
    private Gradient healthGradient;
    private Coroutine healthCoroutine; // Reference to the ongoing coroutine

    public void Start()
    {
        currentHealth = maxHealth;
        InitializeHealthGradient();
        UpdateHealthUI();

        damageCanvasGroup.alpha = 0;
        healCanvasGroup.alpha = 0;
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

        if(damage >= 2)
        {
            soundController.PlayAudioClip(5); // play damage audio clip
            SendHapticFeedback();
            StartCoroutine(ShowDamageEffect());
        }
        
        if (currentHealth <= 0)
        {
            Debug.Log("PLayer Health Reached zero");
            gameController.playerHealthZero();
            Invoke("LoadStartScene", 3.5f);
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
    void LoadStartScene()
    {
        SceneManager.LoadScene("start"); // This will load the scene named "Start"
    }

    public void RestoreHealth()
    {
        soundController.PlayAudioClip(6); // Play healing sound

        StartCoroutine(ShowHealEffect());
        
        if (currentHealth <= 70)
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
        while (damageCanvasGroup.alpha < 1)
        {
            damageCanvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Duration of the effect visible fully

        // Fade out
        while (damageCanvasGroup.alpha > 0)
        {
            damageCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    IEnumerator ShowHealEffect()
    {
        while (healCanvasGroup.alpha < 1)
        {
            healCanvasGroup.alpha += Time.deltaTime / fadeDuration;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f); // Duration of the effect visible fully

        // Fade out
        while (healCanvasGroup.alpha > 0)
        {
            healCanvasGroup.alpha -= Time.deltaTime / fadeDuration;
            yield return null;
        }
    }

    private void SendHapticFeedback()
    {
        // You'll need references to the input devices, typically the controllers
        var devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);

        foreach (var device in devices)
        {
            if (device.isValid)
            {
                // Send a haptic impulse with a given amplitude and duration
                // Adjust amplitude (0.0 to 1.0) and duration as necessary
                HapticCapabilities capabilities;
                if (device.TryGetHapticCapabilities(out capabilities) && capabilities.supportsImpulse)
                {
                    uint channel = 0; // The channel of the haptic motor. Most devices should use 0.
                    float amplitude = 0.5f; // Strength of the vibration
                    float duration = 0.1f; // Duration of the vibration
                    device.SendHapticImpulse(channel, amplitude, duration);
                }
            }
        }
    }
}
