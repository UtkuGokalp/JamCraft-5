using UnityEngine;

namespace JamCraft5.Items
{
    public class ItemConverter
    {
        public static GroundedItem ToGroundedItem(Vector3 spawnPosition, InventoryItem inventoryItem)
        {
            return GroundedItem.Create(spawnPosition, inventoryItem.ItemData);
        }

        #region ToInventoryItem
        public static InventoryItem ToInventoryItem(GroundedItem groundedItem, bool autoDestroyGroundedItem = false)
        {
            if (autoDestroyGroundedItem)
            {
                Object.Destroy(groundedItem.gameObject);
            }
            return new InventoryItem(groundedItem.ItemData);
        }
        #endregion
    }
}
