using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage; // Assign the fill part of your health bar in the Inspector

    public void SetHealth(float current, float max)
    {
        if (fillImage != null && max > 0)
        {
            fillImage.fillAmount = Mathf.Clamp01(current / max);
        }
    }
}