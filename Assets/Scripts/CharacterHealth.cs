using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBar healthBar;

    private bool isColliding = false;
    private Coroutine damageCoroutine;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
            if (enemyHealth != null && enemyHealth.currentHealth > 0)
            {
                isColliding = true;
                damageCoroutine = StartCoroutine(DamageOverTime());
            }
        }
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(10);
            Destroy(collision.gameObject); // optional: destroy projectile on hit
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Object"))
        {
            isColliding = false;

            if (damageCoroutine != null)
                StopCoroutine(damageCoroutine);
        }
    }

    IEnumerator DamageOverTime()
    {
        while (isColliding)
        {
            TakeDamage(10);
            yield return new WaitForSeconds(1f);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            GameOver();
    }

    void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }
}
