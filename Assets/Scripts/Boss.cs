using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;
    public Animator animator;

    public GameObject rock;
    public Transform firePoint;

    private bool inSecondPhase = false;
    private float shootCooldown = 3f;
    private float shootTimer = 0f;

    void Start() {
        animator = GetComponent<Animator>();
        //shootTimer = shootCooldown; // optional: delay first shot
    }

    void Update() {
        LookAtPlayer();

        if (inSecondPhase)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = shootCooldown; // reset the timer ONLY after shooting
            }
        }
    }

    public void EnterSecondStage() {
        inSecondPhase = true;
        shootTimer = shootCooldown; // start cooldown fresh when phase begins
    }

    public void LookAtPlayer()
    {
        if (player != null)
        {
            bool shouldFlip = transform.position.x > player.position.x;

            if (shouldFlip != isFlipped)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
                isFlipped = shouldFlip;
            }
        }
    }

    public void Shoot()
    {
        animator.SetTrigger("MikuBeam");

        if (rock != null && firePoint != null)
        {
            Vector3 spawnOffset = new Vector3(0f, -3f, 0f); // Adjust Y as needed
            Vector3 spawnPosition = firePoint.position + spawnOffset;
            GameObject rockClone = Instantiate(
                rock,
                spawnPosition,
                isFlipped ? Quaternion.Euler(0, 180, 0) : Quaternion.identity
            );

            rockClone.transform.parent = null;
            rockClone.tag = "Projectile";
            Destroy(rockClone, 3f); // self-destruct
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !inSecondPhase)
        {
            animator.SetTrigger("Attack");
        }
    }
}
