using UnityEngine;

public class Collapse : MonoBehaviour
{
    private Rigidbody2D rb;

    [Header("Damage Settings")]
    public float enemyDamage = 25f;
    public float playerDamage = 25f;

    private bool isActive = false; // Only true while falling

    [Header("Ground Settings")]
    [SerializeField] private LayerMask groundLayer; // Assign your ground layer in the Inspector

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Static; // Start as static
    }

    public void ActivateCollapse()
    {
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Dynamic; // Make platform fall
            isActive = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return; // Only deal damage if falling

        // Check if we landed on the ground (using layer)
        if (((1 << collision.gameObject.layer) & groundLayer) != 0)
        {
            // Landed on ground, stop falling and disable damage
            if (rb != null)
                rb.bodyType = RigidbodyType2D.Static;
            isActive = false;
            return; // IMPORTANT: Return immediately, do not process further
        }

        // Only deal damage if still active (not landed)
        if (!isActive)
            return;

        // Deal damage to enemy if present
        EnemyMovement enemy = collision.collider.GetComponent<EnemyMovement>();
        if (enemy != null)
        {
            enemy.TakeDamage(enemyDamage);
        }

        // Deal damage to player if present
        PlayerHealth player = collision.collider.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(playerDamage);
        }
    }

}
