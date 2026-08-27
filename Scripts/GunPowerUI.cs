using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunPowerUI : MonoBehaviour
{
    [Header("Power Settings")]
    [SerializeField] private float maxPower = 100f;
    private float currentPower;

    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI powerText; // For percentage text (e.g., "75%")
    [SerializeField] private Image powerBarFill;        // Optional visual fill bar

    private void Start()
    {
        currentPower = maxPower;
        UpdatePowerUI();
    }

    // Call this method whenever the gun shoots, charges, or recharges
    public void ModifyPower(float amount)
    {
        currentPower += amount;
        currentPower = Mathf.Clamp(currentPower, 0f, maxPower);
        UpdatePowerUI();
    }

    // Call this to set power directly (e.g., set to 50%)
    public void SetPower(float newPower)
    {
        currentPower = Mathf.Clamp(newPower, 0f, maxPower);
        UpdatePowerUI();
    }

    private void UpdatePowerUI()
    {
        float powerPercentage = (currentPower / maxPower) * 100f;

        // Update Text (displays as rounded whole percentage, e.g. "85%")
        if (powerText != null)
        {
            powerText.text = $"{Mathf.RoundToInt(powerPercentage)}%";
        }

        // Update UI Image Fill (0.0 to 1.0 range)
        if (powerBarFill != null)
        {
            powerBarFill.fillAmount = currentPower / maxPower;
        }
    }

    public float GetCurrentPower() => currentPower;
    public bool HasEnoughPower(float cost) => currentPower >= cost;
}