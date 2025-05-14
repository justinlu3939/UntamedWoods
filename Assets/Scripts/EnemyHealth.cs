using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Slider
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    public int currentHealth;
    // private Animator playerAnimator; // REMOVED - Player will handle its own animation
    public Animator bossAnimator;
    private EnemyDrop enemyDrop; // Assuming you have this script for drops
    private bool isInvulnerable = false;
    private bool hasEnteredSecondStage = false;

    private BossMovement bossMovement;
    private Boss boss;
    private bool isPlayerInRange = false;
    private bool bossDefeated = false;

    // --- Health Bar Variables ---
    public Slider healthSliderPrefab;
    public Vector3 healthBarOffset = new Vector3(0, 1.5f, 0);
    private Slider healthSliderInstance;
    private Canvas bossCanvas;
    // --- End Health Bar Variables ---

    // Tinting variables
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color damageTintColor = Color.red;
    public float tintDuration = 0.2f;

    //Set up audio
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioSource secondAudioSource;
    public AudioClip secondPhaseSound;
    public AudioSource thirdAudioSource;
    public AudioClip thirdPhaseSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        // playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>(); // REMOVED
        bossAnimator = GetComponent<Animator>();
        enemyDrop = GetComponent<EnemyDrop>(); // Make sure this script exists or remove if not used
        bossMovement = GetComponent<BossMovement>();
        boss = GetComponent<Boss>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        else
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                originalColor = spriteRenderer.color;
            } else {
                Debug.LogWarning("SpriteRenderer component not found on the boss or its children for tinting.", gameObject);
            }
        }
        SetupHealthBar();
    }

    void SetupHealthBar()
    {
        bossCanvas = GetComponentInChildren<Canvas>();
        if (healthSliderPrefab != null)
        {
            if (bossCanvas == null)
            {
                Debug.LogError("BossHealth: No Canvas found on the Boss GameObject or its children. Please add a World Space Canvas.", gameObject);
                return;
            }
            if (bossCanvas.renderMode != RenderMode.WorldSpace) {
                Debug.LogError("BossHealth: The Canvas on the Boss must be set to RenderMode.WorldSpace.", bossCanvas.gameObject);
                return;
            }

            healthSliderInstance = Instantiate(healthSliderPrefab, bossCanvas.transform);
            RectTransform sliderRect = healthSliderInstance.GetComponent<RectTransform>();
            if (sliderRect != null) {
                sliderRect.localPosition = healthBarOffset;
                sliderRect.localRotation = Quaternion.identity;
                sliderRect.localScale = Vector3.one;
            } else {
                 Debug.LogError("Health Slider Prefab does not have a RectTransform component.", healthSliderPrefab);
            }
            healthSliderInstance.gameObject.SetActive(true);
            UpdateHealthBarDisplay();
        }
        else
        {
            Debug.LogWarning("Health Slider Prefab not assigned in the Inspector for EnemyHealth.", gameObject);
        }
    }

    void UpdateHealthBarDisplay()
    {
        if (healthSliderInstance != null)
        {
            healthSliderInstance.maxValue = maxHealth;
            healthSliderInstance.value = currentHealth;
        }
    }

    void Update()
    {
        if (bossDefeated && isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("Game Win");
        }
    }

    // OnMouseDown() REMOVED - Player script will now initiate damage
    // void OnMouseDown()
    // {
    //     TakeDamage(10);
    //     // playerAnimator.SetTrigger("Attack"); // This will be handled by the player's script
    // }

    public void TakeDamage(int damage) // Made public if it wasn't already, so player script can call it
    {
        if (isInvulnerable || currentHealth <= 0)
            return;

        currentHealth -= damage;
        ApplyDamageTint();
        UpdateHealthBarDisplay();

        // Play damage sound
        audioSource.PlayOneShot(damageSound);

        if (currentHealth <= maxHealth / 2 && !hasEnteredSecondStage)
        {
            EnterSecondStage();
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHealthBarDisplay();
            bossAnimator.SetTrigger("Die");

            thirdAudioSource.PlayOneShot(thirdPhaseSound);
            
            if (boss != null) boss.enabled = false;
            if (bossMovement != null) bossMovement.enabled = false;
            
            bossDefeated = true;

            if (healthSliderInstance != null)
            {
                // Optional: Delay destroy for animation, or destroy immediately
                float dieAnimationLength = 0;
                AnimatorStateInfo stateInfo = bossAnimator.GetCurrentAnimatorStateInfo(0);
                // Check if the "Die" state is actually playing or queued. This check might need refinement.
                // A simpler way is to have a fixed time if your die animation length is consistent.
                // For now, let's use a default of 2 seconds if animation length is not easily available or 0.
                // if (bossAnimator.GetCurrentAnimatorStateInfo(0).IsName("Die")) { // This checks current state, "Die" trigger might take a frame
                //    dieAnimationLength = bossAnimator.GetCurrentAnimatorStateInfo(0).length;
                // }
                // A more robust way for animation length: add a public float dieAnimationDuration to inspect and use that.
                // Or simply destroy it after a fixed delay.
                Destroy(healthSliderInstance.gameObject, 2f); 
            }
        }
    }

    void ApplyDamageTint()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageTintColor;
            StartCoroutine(RevertColorAfterDelay(tintDuration));
        }
    }

    IEnumerator RevertColorAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (spriteRenderer != null)
        {
            spriteRenderer.color = originalColor;
        }
    }

    void EnterSecondStage()
    {
        Debug.Log("Second Stage Triggered");
        secondAudioSource.PlayOneShot(secondPhaseSound);
        isInvulnerable = true;
        hasEnteredSecondStage = true;
        bossAnimator.SetTrigger("SecondStage");

        StartCoroutine(FreezeBossForSeconds(3f)); // Standard invulnerability duration for phase change
        // Invoke DisableInvulnerability AFTER the freeze and animation for SecondStage are expected to be done.
        // If SecondStage animation is 3s, then invulnerability also lasts 3s from this call.
        Invoke(nameof(DisableInvulnerability), 3f);
    }

    IEnumerator FreezeBossForSeconds(float seconds)
    {
        if (bossMovement != null)
            bossMovement.enabled = false;
        yield return new WaitForSeconds(seconds);
        if (bossMovement != null && currentHealth > 0) // Only re-enable if not dead
            bossMovement.enabled = true;
    }

    void DisableInvulnerability()
    {
        isInvulnerable = false;
        if(boss != null && currentHealth > 0) { // Check currentHealth > 0 before allowing second stage logic to fully kick in
            Debug.Log("Boss Invulnerability Disabled, entering second stage logic.");
            boss.EnterSecondStage(); // This is where the boss script's second phase logic starts (like shooting)
        } else if (boss != null && currentHealth <=0) {
            Debug.Log("Boss was defeated before invulnerability for second stage ended.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player in range for scene interaction (E key).");
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    void OnDestroy() {
        if (healthSliderInstance != null) {
            Destroy(healthSliderInstance.gameObject);
        }
    }
}