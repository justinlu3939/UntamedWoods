using UnityEngine;

public class rockScript : MonoBehaviour
{
    public float speed = 2f;

    void Start()
    {
        // Destroy the laser after 3 seconds
        Destroy(gameObject, 3f);
    }

    void Update()
    {
        // Move the laser forward
        transform.Translate(Vector2.right * speed * Time.deltaTime, Space.Self);

    }
}
