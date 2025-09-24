using UnityEngine;

public class DragObject2D : MonoBehaviour
{
    [Header("Drag Settings")]
    [SerializeField] private float dragSpeed = 10f;
    [SerializeField] private bool maintainOffset = true;

    private Rigidbody2D rb;
    private Vector3 offset;
    private bool isDragging = false;
    private Camera mainCamera;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
    }

    private void OnMouseDown()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        // Calculate offset between mouse position and object position
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (maintainOffset)
        {
            offset = transform.position - mousePos;
        }
        else
        {
            offset = Vector3.zero;
        }

        isDragging = true;
        
        // Optional: Change physics behavior while dragging
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    private void OnMouseDrag()
    {
        if (!isDragging) return;

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 targetPosition = mousePos + offset;

        if (rb != null)
        {
            // Use physics-based movement for realistic dragging
            Vector3 force = (targetPosition - transform.position) * dragSpeed;
            rb.linearVelocity = force;
        }
        else
        {
            // Fallback to direct movement if no Rigidbody2D
            transform.position = targetPosition;
        }
    }

    private void OnMouseUp()
    {
        isDragging = false;
        
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

}