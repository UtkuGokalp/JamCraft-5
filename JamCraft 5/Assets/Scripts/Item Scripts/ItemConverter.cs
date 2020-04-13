using UnityEngine;

namespace JamCraft5.Items
{
    public class ItemConverter
    {
        #region ToGroundedItem
        public static GroundedItem ToGroundedItem(Vector3 spawnPosition, InventoryItem inventoryItem)
        {
            return Object.Instantiate(inventoryItem.ItemData.groundedItemPrefab, spawnPosition, inventoryItem.ItemData.groundedItemPrefab.transform.rotation);
        }
        #endregion

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
