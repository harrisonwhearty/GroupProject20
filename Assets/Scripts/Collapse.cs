using UnityEngine;

public class Collapse : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Static; // Start as static
    }

    public void ActivateCollapse()
    {
        if (rb != null)
            rb.bodyType = RigidbodyType2D.Dynamic; // Make platform fall
    }
}