using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private GameObject[] objectsToActivate; // Optional: still activates objects
    [SerializeField] private Collapse platformToCollapse;     // Optional: still collapses platform
    [SerializeField] private Transform objectToMove;          // The object to move up/down
    [SerializeField] private Vector3 upPositionOffset = new Vector3(0, 2, 0); // Offset for "up" position
    [SerializeField] private float moveSpeed = 5f;            // Speed of movement

    private bool isUp = false;
    private Vector3 initialPosition;
    private bool isMoving = false;
    private Vector3 targetPosition;

    private void Start()
    {
        if (objectToMove != null)
            initialPosition = objectToMove.position;
    }

    private void Update()
    {
        // Smoothly move the object if needed
        if (isMoving && objectToMove != null)
        {
            objectToMove.position = Vector3.MoveTowards(objectToMove.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(objectToMove.position, targetPosition) < 0.01f)
                isMoving = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Arrow"))
        {
            // Activate all assigned GameObjects
            foreach (var obj in objectsToActivate)
            {
                if (obj != null)
                    obj.SetActive(true);
            }

            // Trigger platform collapse if assigned
            if (platformToCollapse != null)
            {
                platformToCollapse.ActivateCollapse();
            }

            // Toggle object up/down
            if (objectToMove != null)
            {
                isUp = !isUp;
                targetPosition = isUp ? initialPosition + upPositionOffset : initialPosition;
                isMoving = true;
            }
        }
    }
}