using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float trackingRange = 5f;

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float distanceToPlayer = Mathf.Abs(player.position.x - transform.position.x);

        if (distanceToPlayer < trackingRange)
        {
            float movementDirection = Mathf.Sign(player.position.x - transform.position.x);
            transform.Translate(new Vector2(movementDirection, 0) * moveSpeed * Time.deltaTime);
        }
    }
}
