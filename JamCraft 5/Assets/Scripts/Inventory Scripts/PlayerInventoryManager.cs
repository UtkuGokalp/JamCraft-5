using UnityEngine;
using JamCraft5.Items;
using JamCraft5.Inventory;
using System.Collections.Generic;

namespace JamCraft5.Player.Inventory
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private InventoryItem[] startingItems;
        private List<InventorySlot> inventory;
        public List<InventorySlot> Inventory
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new List<InventorySlot>();
                    foreach (InventoryItem item in startingItems)
                    {
                        inventory.Add(new InventorySlot() { ContainedItem = item });
                    }
                }
                return inventory;
            }
        }
        #endregion

        #region Awake
        private void Awake()
        {
            foreach (InventoryItem item in startingItems)
            {
                AddItem(item);
            }
        }
        #endregion

        #region AddItem
        /// <summary>
        /// Adds the given item to inventory if possible. Returns false otherwise.
        /// </summary>
        /// <returns></returns>
        public (bool added, InventoryItem addedItem) AddItem(InventoryItem item)
        {
            var inventoryItemData = HasItem(item);
            if (inventoryItemData.contains)
            {
                inventoryItemData.slotContained.ItemCount++;
                return System.ValueTuple.Create(true, item);
            }
            else
            {
                Inventory.Add(new InventorySlot());
                InventorySlot lastSlot = Inventory[Inventory.Count - 1];
                lastSlot.ContainedItem = item;
                lastSlot.ItemCount = 1;
                return System.ValueTuple.Create(true, item);
            }
        }
        #endregion

        #region RemoveItem
        /// <summary>
        /// Tries to remove item from inventory. Returns false if item does not exist or there are less items in the inventory than requested number.
        /// </summary>
        public (bool removed, InventoryItem removedItem) RemoveItem(InventoryItem item, int itemsToRemove)
        {
            var inventoryItemData = HasItem(item);
            if (itemsToRemove < 0)
            {
                throw new System.ArgumentOutOfRangeException("Items to remove can not be less than zero.");
            }
            if (inventoryItemData.contains && inventoryItemData.slotContained.ItemCount >= itemsToRemove)
            {
                inventoryItemData.slotContained.ItemCount -= itemsToRemove;
                if (inventoryItemData.slotContained.ItemCount == 0)
                {
                    Inventory.Remove(inventoryItemData.slotContained);
                }
                return System.ValueTuple.Create(true, item);
            }
            return System.ValueTuple.Create<bool, InventoryItem>(false, null);
        }
        #endregion

        #region HasItem
        private (bool contains, InventorySlot slotContained) HasItem(InventoryItem item)
        {
            foreach (InventorySlot slot in Inventory)
            {
                if (slot.ContainedItem == null)
                {
                    continue;
                }
                if (slot.ContainedItem.ItemData.ID == item.ItemData.ID)
                {
                    return System.ValueTuple.Create(true, slot);
                }
            }
            return System.ValueTuple.Create<bool, InventorySlot>(false, null);
        }
        #endregion
    }
}
