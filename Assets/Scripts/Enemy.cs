using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float patrolTime = 2.0f;
    public float detectionRange = 5.0f;

    [Header("Health Settings")]
    public float maxHealth = 100.0f;
    public float CurrentHealth { get; private set; }

    [Header("Damage Settings")]
    public float arrowDamage = 25.0f;
    public float playerDamage = 10.0f;

    private Animator animator;
    private float direction = -1f;
    private float patrolTimer;
    private Transform player;
    private bool isChasing = false;

    // Store reference to the player in range for attack
    private PlayerHealth playerHealthInRange;

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
            isChasing = true;
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            transform.position += directionToPlayer * speed * Time.deltaTime;

            if (animator != null)
            {
                animator.SetFloat("MoveX", Mathf.Sign(directionToPlayer.x));
            }
        }
        else
        {
            isChasing = false;
            transform.position += Vector3.right * direction * speed * Time.deltaTime;

            if (animator != null)
            {
                animator.SetFloat("MoveX", direction);
            }

            patrolTimer -= Time.deltaTime;
            if (patrolTimer <= 0f)
            {
                direction *= -1f;
                patrolTimer = patrolTime;
            }
        }
    }

    bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, player.position) <= detectionRange;
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("AttackRight");
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
            Destroy(collision.collider.gameObject);
        }
        else if (collision.collider.CompareTag("Player"))
        {
            if (animator != null && player != null)
            {
                float directionToPlayer = player.position.x - transform.position.x;
                if (directionToPlayer < 0)
                {
                    animator.SetTrigger("Attack");
                }
                else
                {
                    animator.SetTrigger("AttackRight");
                }
            }

            // Store reference to the player's health script
            playerHealthInRange = collision.collider.GetComponent<PlayerHealth>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerHealthInRange = null;
        }

    }

    // Call this from an Animation Event in both attack animations
    public void DealDamageToPlayer()
    {
        if (playerHealthInRange != null)
        {
            playerHealthInRange.TakeDamage(playerDamage);
        }
    }
}

