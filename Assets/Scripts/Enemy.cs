using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float patrolTime = 2.0f; // Time to move in one direction before turning
    public float detectionRange = 5.0f; // Detection radius for chasing the player

    [Header("Health Settings")]
    public float maxHealth = 100.0f;
    public float CurrentHealth { get; private set; }

    [Header("Damage Settings")]
    public float arrowDamage = 25.0f; // Default damage taken from an arrow
    public float playerDamage = 10.0f; // Damage dealt to the player

    private Animator animator;
    private float direction = -1f; // Start moving left
    private float patrolTimer;
    private Transform player;
    private bool isChasing = false;

    void Start()
    {
        CurrentHealth = maxHealth;
        animator = GetComponent<Animator>();
        patrolTimer = patrolTime;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player != null && IsPlayerInRange())
        {
            // Chase the player
            isChasing = true;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * speed * Time.deltaTime;

            // Set animation parameter for direction (right: 1, left: -1)
            if (animator != null)
            {
                animator.SetFloat("MoveX", Mathf.Sign(directionToPlayer.x));
            }
        }
        else
        {
            // Patrol left and right
            isChasing = false;
            transform.position += Vector3.right * direction * speed * Time.deltaTime;

            if (animator != null)
            {
                animator.SetFloat("MoveX", direction);
            }

            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0f)
            {
                direction *= -1f; // Reverse direction
                patrolTimer = patrolTime;
                // Optional: flip sprite if using SpriteRenderer
                // GetComponent<SpriteRenderer>().flipX = direction > 0;
            }
        }
    }

    bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        if (CurrentHealth < 0) CurrentHealth = 0;

        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > maxHealth) CurrentHealth = maxHealth;
    }

    public bool IsAlive()
    {
        return CurrentHealth > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            TakeDamage(arrowDamage);
            Destroy(collision.collider.gameObject); // Optionally destroy the arrow on hit
        }
        else if (collision.collider.CompareTag("Player"))
        {
            // Trigger the attack/hurt animation
            if (animator != null)
            {
                animator.SetTrigger("Attack");
            }

            // Optionally, damage the player if they have a PlayerHealth script
            PlayerHealth playerHealth = collision.collider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(playerDamage);
            }
        }
    }
}
