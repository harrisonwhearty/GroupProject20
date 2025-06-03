using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float maxLifetime = 3f;      // Time in seconds before despawn
    [SerializeField] private float maxDistance = 20f;     // Max distance arrow can travel

    private Vector3 spawnPosition;
    private float spawnTime;

    private void Start()
    {
        spawnPosition = transform.position;
        spawnTime = Time.time;
    }

    private void Update()
    {
        // Despawn after maxLifetime seconds
        if (Time.time - spawnTime >= maxLifetime)
        {
            Destroy(gameObject);
            return;
        }

        // Despawn if traveled beyond maxDistance
        if (Vector3.Distance(spawnPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject);
        }
    }
}