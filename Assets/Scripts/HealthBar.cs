using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillImage; // Assign the fill part of your health bar in the Inspector

    // Define the fillAmount for each heart count (index 0 = 0 hearts, index 4 = 4 hearts)
    [SerializeField] private float[] fillAmounts = { 0f, 0.35f, 0.6f, 0.815f, 1f};

    public void SetHealth(float current, float max)
    {
        if (fillImage != null && max > 0)
        {
            float hearts = Mathf.Clamp(current, 0, max);
            int lower = Mathf.FloorToInt(hearts);
            int upper = Mathf.CeilToInt(hearts);

            if (lower == upper)
            {
                fillImage.fillAmount = fillAmounts[lower];
            }
            else
            {
                float t = hearts - lower;
                fillImage.fillAmount = Mathf.Lerp(fillAmounts[lower], fillAmounts[upper], t);
            }
        }
    }
}