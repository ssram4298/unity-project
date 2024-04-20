using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    public Image healthBarFill; // Assign this in the inspector to your health bar fill
    public Color fullHealthColor = Color.green;
    public Color zeroHealthColor = Color.red;

    // Call this method with the current health value, maxHealth is the health at full bar
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
        healthBarFill.color = Color.Lerp(zeroHealthColor, fullHealthColor, currentHealth / maxHealth);
    }
}
