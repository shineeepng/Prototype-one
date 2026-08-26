using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Health targetHealth;
    public Slider healthSlider;

    private void Start()
    {
        if (healthSlider == null)
        {
            healthSlider = GetComponent<Slider>();
        }

        healthSlider.maxValue = targetHealth.maxHealth;
        healthSlider.value = targetHealth.currentHealth;
    }

    private void Update()
    {
        if (targetHealth == null) return;

        healthSlider.value = targetHealth.currentHealth;
    }
}
