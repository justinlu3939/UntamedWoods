using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI quantityText;
    public Image itemImage;

    public void SetSlot(string itemName, int quantity, Sprite itemSprite)
    {
        itemNameText.text = itemName;
        quantityText.text = quantity.ToString();
        itemImage.sprite = itemSprite;
    }
}
