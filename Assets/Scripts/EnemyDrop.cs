using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    public GameObject[] dropItems; // Array to hold different drop items
    public float dropChance = 0.5f; // Chance for an item to drop

    public void DropItem()
    {
        if (dropItems.Length == 0) return;

        float randomValue = Random.Range(0f, 1f);
        if (randomValue <= dropChance)
        {
            int dropIndex = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[dropIndex], transform.position, Quaternion.identity);
        }
    }
}
