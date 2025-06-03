using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Transform[] patrolPoints;
    private int currentPatrolIndex = 0;

    [Header("Detection & Attack")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1.2f;
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int attackDamage = 1;
    [SerializeField] private LayerMask playerLayer;

    private Transform player;
    private Animator anim;
    private float lastAttackTime;
    private bool playerInSight = false;
    private EnemyHealth health;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        health = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    private void Update()
    {
        if (health != null && health.enabled && !IsDead())
        {
            DetectPlayer();

            if (playerInSight)
            {
                float dist = Vector2.Distance(transform.position, player.position);
                if (dist > attackRange)
                {
                    MoveTowards(player.position);
                    anim.SetBool("attack", false);
                }
                else
                {
                    anim.SetBool("attack", true);
                    Attack();
                }
            }
            else
            {
                Patrol();
                anim.SetBool("attack", false);
            }
        }
    }

    private void DetectPlayer()
    {
        if (player == null)
        {
            playerInSight = false;
            return;
        }
        float dist = Vector2.Distance(transform.position, player.position);
        playerInSight = dist <= detectionRange;
    }

    private void MoveTowards(Vector3 target)
    {
        Vector3 dir = (target - transform.position).normalized;
        transform.position += dir * moveSpeed * Time.deltaTime;
        // Optional: Flip sprite based on direction
        if (dir.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(dir.x), 1, 1);
    }

    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;
        Transform targetPoint = patrolPoints[currentPatrolIndex];
        MoveTowards(targetPoint.position);

        if (Vector2.Distance(transform.position, targetPoint.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        lastAttackTime = Time.time;

        // Damage player if in range
        Collider2D hit = Physics2D.OverlapCircle(transform.position, attackRange, playerLayer);
        if (hit != null && hit.CompareTag("Player"))
        {
            // You should have a PlayerHealth script with TakeDamage(int)
            hit.GetComponent<PlayerHealth>()?.TakeDamage(attackDamage);
        }
    }

    private bool IsDead()
    {
        return health != null && health.enabled && health.GetType().GetField("isDead", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(health) is bool b && b;
    }

    // Optional: Visualize detection/attack range in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}