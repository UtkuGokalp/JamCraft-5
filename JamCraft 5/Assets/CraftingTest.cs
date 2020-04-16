using UnityEngine;
using JamCraft5.Items;
using JamCraft5.Crafting;
using Utility.Development;
using JamCraft5.Player.Inventory;

public class CraftingTest : MonoBehaviour
{
    [SerializeField]
    private Craftable craftingObject;
    private PlayerInventoryManager playerInventory;

    private void Awake()
    {
        playerInventory = FindObjectOfType<PlayerInventoryManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButton.MIDDLE))
        {
            InventoryItem craftedItem = craftingObject.Craft(playerInventory);
            if (craftedItem != null)
            {
                playerInventory.AddItem(craftedItem);
            }
        }
    }
}
