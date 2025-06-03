using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2.0f;
    public float detectionRange = 10.0f;

    [Header("Health Settings")]
    public float maxHealth = 100.0f;
    public float CurrentHealth { get; private set; }

    [Header("Damage Settings")]
    public float arrowDamage = 25.0f; // Default damage taken from an arrow

    private Transform target;

    void Start()
    {
        CurrentHealth = maxHealth;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    void Update()
    {
        if (target != null && IsTargetDetected())
        {
            MoveTowardsTarget();
        }
    }

    bool IsTargetDetected()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        return distance <= detectionRange;
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
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
    }
}