using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public HealthBar healthBar; // Assign in Inspector if you want to update a health bar

    [Header("Animation")]
    [SerializeField] private Animator animator; // Assign in Inspector

    [Header("References")]
    [SerializeField] private MonoBehaviour[] scriptsToDisable; // Assign your control scripts in Inspector

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return; // Prevent further damage after death

        currentHealth -= amount;
        if (currentHealth < 0) currentHealth = 0;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        if (animator != null)
        {
            animator.SetBool("IsDead", true); // Use a bool parameter named "IsDead"
        }
        // Disable player control scripts
        foreach (var script in scriptsToDisable)
        {
            if (script != null)
                script.enabled = false;
        }
    }
}
