using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab; // Reference to the arrow prefab
    [SerializeField] private Transform arrowSpawnPoint; // Where the arrow will spawn
    [SerializeField] private float arrowSpeed = 20f; // Speed of the arrow
    [SerializeField] private float fireCooldown = 0.5f; // Time between shots

    private float lastFireTime; // Tracks the last time an arrow was fired

    // Update is called once per frame
    void Update()
    {
        // Check for input to shoot and if cooldown has passed
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + fireCooldown)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        // Instantiate the arrow at the spawn point
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        // Add velocity to the arrow to make it move
        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = arrowSpawnPoint.right * arrowSpeed; // Assumes the arrow faces right
        }

        // Update the last fire time
        lastFireTime = Time.time;
    }
}