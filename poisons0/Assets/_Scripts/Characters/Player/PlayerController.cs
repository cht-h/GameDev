using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float deceleration = 15f;
    [SerializeField, Tooltip("Interaction detection range")]
    private float interactionRange = 1.5f;

    [Header("References")]
    [SerializeField] private Transform visuals;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject interactionHint;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private bool isFacingRight = true;
    private IInteractable currentInteractable;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        interactionHint.SetActive(false);
    }

    private void Update()
    {
        HandleMovement();
        CheckInteractions();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        Vector2 input = InputManager.Instance.MoveInput;

        currentVelocity = Vector2.Lerp(
            currentVelocity,
            input.normalized * moveSpeed,
            acceleration * Time.deltaTime
        );

        rb.linearVelocity = currentVelocity;
    }

    private void CheckInteractions()
    {
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            direction,
            interactionRange,
            LayerMask.GetMask("Interactable"));

        if (hit.collider != null && hit.collider.TryGetComponent(out IInteractable interactable))
        {
            if (interactable != currentInteractable)
            {
                currentInteractable?.HidePrompt();
                currentInteractable = interactable;
                interactionHint.SetActive(true);
            }

            if (InputManager.Instance.IsInteracting)
                interactable.OnInteract(this);
        }
        else if (currentInteractable != null)
        {
            currentInteractable.HidePrompt();
            interactionHint.SetActive(false);
            currentInteractable = null;
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = visuals.localScale;
        scale.x *= -1;
        visuals.localScale = scale;
    }

    private void UpdateAnimation()
    {
        if (animator != null)
            animator.SetBool("IsMoving", currentVelocity.magnitude > 0.1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Vector2 direction = isFacingRight ? Vector2.right : Vector2.left;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)direction * interactionRange);
    }
}