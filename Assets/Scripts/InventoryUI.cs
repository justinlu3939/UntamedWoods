using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform slotParent;
    public Inventory playerInventory;
    public GameObject inventoryPanel; // The panel that contains the inventory UI
    public int totalSlots = 6; // Total number of slots to display

    void Start()
    {
        inventoryPanel.SetActive(false); // Hide inventory at the start
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inventoryPanel.activeSelf)
            {
                CloseInventory();
            }
        }
    }

    void ToggleInventory()
    {
        bool isActive = !inventoryPanel.activeSelf;
        inventoryPanel.SetActive(isActive);

        if (isActive)
        {
            Time.timeScale = 0; // Pause the game
            UpdateInventoryUI();
        }
        else
        {
            Time.timeScale = 1; // Resume the game
        }
    }

    void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

    void UpdateInventoryUI()
    {
        foreach (Transform child in slotParent)
        {
            if (child.CompareTag("InventorySlot"))
            {
                Destroy(child.gameObject);
            }
        }

        int slotIndex = 0;
        foreach (Inventory.InventoryItem item in playerInventory.items)
        {
            GameObject slot = Instantiate(slotPrefab, slotParent);
            InventorySlot slotScript = slot.GetComponent<InventorySlot>();
            slotScript.SetSlot(item.itemName, item.quantity, item.itemSprite);
            slotIndex++;
        }

        for (; slotIndex < totalSlots; slotIndex++)
        {
            Instantiate(slotPrefab, slotParent);
        }
    }
}
