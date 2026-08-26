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
            hpSlider.maxValue = maxHealth;
            hpSlider.value = currentHealth;
        }
    }

   
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

       
        if (hpSlider != null)
        {
            hpSlider.value = currentHealth;
        }

        
        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Pelaaja kuoli!");
        gameObject.SetActive(false); 
    }
}

