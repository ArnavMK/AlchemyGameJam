using UnityEngine;

public class ObjectWobble : MonoBehaviour
{
    [Header("Wobble Settings")]
    [SerializeField] private float wobbleRadius = 0.2f;  // How far the object can move from its original position
    [SerializeField] private float wobbleSpeed = 3f;     // Speed of wobble
    [SerializeField] private bool randomizeDirection = true; // Should wobble pick new random directions?

    private Vector3 originalPos;
    private Vector3 targetOffset;

    private void Start()
    {
        originalPos = transform.localPosition;
        PickNewTarget();
    }

    private void Update()
    {
        // Move toward the target wobble position
        transform.localPosition = Vector3.Lerp(
            transform.localPosition, 
            originalPos + targetOffset, 
            Time.deltaTime * wobbleSpeed
        );

        // If we're close enough, pick a new random direction
        if (Vector3.Distance(transform.localPosition, originalPos + targetOffset) < 0.01f)
        {
            PickNewTarget();
        }
    }

    private void PickNewTarget()
    {
        if (randomizeDirection)
        {
            // Random inside a circle for 2D wobble
            Vector2 random2D = Random.insideUnitCircle * wobbleRadius;
            targetOffset = new Vector3(random2D.x, random2D.y, 0f);
        }
        else
        {
            // Simple up-down wobble if you don’t want randomness
            targetOffset = new Vector3(0f, Random.Range(-wobbleRadius, wobbleRadius), 0f);
        }
    }
}
