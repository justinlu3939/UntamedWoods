using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 3f;
    public float trackingRange = 5f;

    private Vector2 movementDirection;
    private float timer;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetRandomDirection();
        timer = changeDirectionTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SetRandomDirection();
            timer = changeDirectionTime;
        }
        Move();
    }

    void SetRandomDirection()
    {
        movementDirection = Random.insideUnitCircle.normalized;
    }

    void Move()
    {
        Vector2 targetDirection = (player.position - transform.position).normalized;
        if (Vector2.Distance(transform.position, player.position) < trackingRange)
        {
            movementDirection = Vector2.Lerp(movementDirection, targetDirection, 0.1f).normalized;
        }

        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
    }
}
