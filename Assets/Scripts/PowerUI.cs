using UnityEngine;
using TMPro;

public class PowerUI : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TextMeshProUGUI powerText;

    void Start()
    {
        if (powerText != null)
            powerText.gameObject.SetActive(false);
    }

    void Update()
    {
        if (player == null || powerText == null) return;

        
        float minPower = 1f;
        float maxPower = 100f;

       
        if (player.Power > minPower)
        {
            if (!powerText.gameObject.activeSelf)
                powerText.gameObject.SetActive(true);

            
            float percent = ((player.Power - minPower) / (maxPower - minPower)) * 100f;
            powerText.text = $"{Mathf.RoundToInt(percent)}%";
        }
        else
        {
            if (powerText.gameObject.activeSelf)
                powerText.gameObject.SetActive(false);
        }
    }
}