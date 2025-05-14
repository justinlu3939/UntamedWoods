using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public class InventoryItem
    {
        public string itemName;
        public int quantity;
        public Sprite itemSprite;

        public InventoryItem(string name, int qty, Sprite sprite)
        {
            itemName = name;
            quantity = qty;
            itemSprite = sprite;
        }
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            // Assuming the item GameObject has an Item component with itemName, quantity, and itemSprite fields
            Item item = collision.gameObject.GetComponent<Item>();
            if (item != null)
            {
                AddItem(item.itemName, item.quantity, item.itemSprite);
                Destroy(collision.gameObject); // Destroy the item GameObject after collecting it
            }
        }
    }

    public void AddItem(string itemName, int quantity, Sprite itemSprite)
    {
        InventoryItem item = items.Find(x => x.itemName == itemName);
        if (item != null)
        {
            item.quantity += quantity;
        }
        else
        {
            items.Add(new InventoryItem(itemName, quantity, itemSprite));
        }
    }

    public void RemoveItem(string itemName, int quantity)
    {
        InventoryItem item = items.Find(x => x.itemName == itemName);
        if (item != null)
        {
            item.quantity -= quantity;
            if (item.quantity <= 0)
            {
                items.Remove(item);
            }
        }
    }
}
