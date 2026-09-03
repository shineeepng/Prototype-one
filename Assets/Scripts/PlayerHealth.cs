using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Slider hpSlider;

    private void Start()
    {
        currentHealth = maxHealth;

        if (hpSlider != null)
        {
            hpSlider.minValue = 0f;
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
    }

    public void TakeDamage(float damage)
    {
        damage = Mathf.Abs(damage);

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateSlider();

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        amount = Mathf.Abs(amount);

        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        UpdateSlider();
    }

    private void UpdateSlider()
    {
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }
    }

    private void Die()
    {
        Debug.Log("Pelaaja kuoli!");
        gameObject.SetActive(false);
    }
}