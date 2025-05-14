/*using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : MonoBehaviour
{
    public Inventory inventory;
    public InventorySlot[] craftingSlots;
    public InventorySlot resultSlot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CraftItem();
        }
    }

    void CraftItem()
    {
        // Placeholder logic for crafting items (you can modify this to fit your game)
        List<Inventory.InventoryItem> itemsToCraft = new List<Inventory.InventoryItem>();
        foreach (var slot in craftingSlots)
        {
            if (slot.HasItem())
            {
                itemsToCraft.Add(slot.GetItem());
            }
        }

        // Example crafting recipe: Combine two items with specific names to create a new item
        if (itemsToCraft.Count == 2 && itemsToCraft[0].itemName == "Eggplant" && itemsToCraft[1].itemName == "Apple")
        {
            Inventory.InventoryItem newItem = new Inventory.InventoryItem("Apple", 1, itemsToCraft[1].itemSprite);
            resultSlot.SetSlot(newItem.itemName, newItem.quantity, newItem.itemSprite);
            ClearCraftingSlots();
        }
    }

    void ClearCraftingSlots()
    {
        foreach (var slot in craftingSlots)
        {
            slot.ClearSlot();
        }
    }
}
*/