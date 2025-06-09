using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] private GameObject arrowPrefab; // Reference to the arrow prefab
    [SerializeField] private Transform arrowSpawnPoint; // Where the arrow will spawn
    [SerializeField] private float minArrowSpeed = 10f; // Minimum speed
    [SerializeField] private float maxArrowSpeed = 40f; // Maximum speed
    [SerializeField] private float maxChargeTime = 2f;  // Max time to reach max speed
    [SerializeField] private Animator animator; // Reference to the Animator

    private float chargeTime = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Start charging
            isCharging = true;
            chargeTime = 0f;
            if (animator != null)
                animator.SetBool("IsCharging", true);
        }

        if (isCharging && Input.GetMouseButton(0))
        {
            // Increase charge time, but clamp to maxChargeTime
            chargeTime += Time.deltaTime;
            if (chargeTime > maxChargeTime)
                chargeTime = maxChargeTime;
        }

        if (isCharging && Input.GetMouseButtonUp(0))
        {
            // Release and shoot
            float chargePercent = Mathf.Clamp01(chargeTime / maxChargeTime);
            float arrowSpeed = Mathf.Lerp(minArrowSpeed, maxArrowSpeed, chargePercent);
            Shoot(arrowSpeed);
            isCharging = false;
            if (animator != null)
                animator.SetBool("IsCharging", false);
        }

        // If charging was interrupted (e.g., player releases button early)
        if (isCharging && !Input.GetMouseButton(0))
        {
            isCharging = false;
            if (animator != null)
                animator.SetBool("IsCharging", false);
        }
    }

    private void Shoot(float arrowSpeed)
    {
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);

        Rigidbody2D rb = arrow.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = arrowSpawnPoint.right * arrowSpeed; // Assumes the arrow faces right
        }
    }
}