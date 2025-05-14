using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public float playerAttackReach = 2.0f; // How close the player character needs to be to the boss to hit it.
    public float clickAttackRadius = 1.0f; // How close the mouse click (in world space) needs to be to the boss.
    public int attackDamage = 10;
    public LayerMask bossLayer; // Set this in the Inspector to the layer your Boss is on.

    private Animator playerAnimator;
    private Transform playerTransform;

    void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Assumes Player has an Animator
        playerTransform = transform;

        if (playerAnimator == null)
        {
            Debug.LogWarning("PlayerAttackController: Player does not have an Animator component.");
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        Vector2 clickWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Find all colliders within the clickAttackRadius around the mouse click position
        // that are on the bossLayer.
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(clickWorldPosition, clickAttackRadius, bossLayer);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // Check if the hit object is indeed the boss (e.g., by tag or by having EnemyHealth script)
            EnemyHealth bossHealth = hitCollider.GetComponent<EnemyHealth>();
            if (bossHealth != null) // We found a boss (or something with EnemyHealth)
            {
                // Now check if the player character is close enough to this boss
                float distanceToBoss = Vector2.Distance(playerTransform.position, hitCollider.transform.position);

                if (distanceToBoss <= playerAttackReach)
                {
                    // Player is in reach, and click was near boss. Attack!
                    if (playerAnimator != null)
                    {
                        playerAnimator.SetTrigger("Attack"); // Trigger player's attack animation
                    }
                    bossHealth.TakeDamage(attackDamage);
                    Debug.Log("Player attacked boss for " + attackDamage + " damage.");
                    return; // Attack one boss per click, if multiple are in range (unlikely for a single boss)
                }
                else
                {
                    Debug.Log("Player clicked near boss, but player character is too far to attack.");
                    // Optional: feedback like "Too far!"
                }
            }
        }
    }

    // Optional: Visualize the attack ranges in the editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        if (playerTransform == null) playerTransform = transform; // For editor preview
        Gizmos.DrawWireSphere(playerTransform.position, playerAttackReach);

        // Note: ClickAttackRadius is harder to visualize constantly without mouse position,
        // but you can draw it if you store last click position or similar for debugging.
    }
}