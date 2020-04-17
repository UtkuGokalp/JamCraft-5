using UnityEngine;

namespace JamCraft5.Items
{
    public static class ItemConverter
    {
        #region ToGroundedItem
        public static GroundedItem ToGroundedItem(Vector3 spawnPosition, InventoryItem inventoryItem)
        {
            if (inventoryItem.ItemData.groundedItemPrefab == null)
            {
                Debug.LogWarning($"Grounded Item Prefab of {inventoryItem.ItemData.name} is null.");
                return null;
            }
            return Object.Instantiate(inventoryItem.ItemData.groundedItemPrefab, spawnPosition, inventoryItem.ItemData.groundedItemPrefab.transform.rotation);
        }

        public static GroundedItem ToGroundedItem(Vector3 spawnPosition, InventoryWeapon inventoryWeapon)
        {
            return ToGroundedItem(spawnPosition, ToInventoryItem(inventoryWeapon));
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

        public static InventoryItem ToInventoryItem(InventoryWeapon inventoryWeapon)
        {
            return new InventoryItem(inventoryWeapon.ItemData);
        }
        #endregion

        #region ToInventoryWeapon
        public static InventoryWeapon ToInventoryWeapon(GroundedItem groundedItem, bool autoDestroyGroundedItem = false)
        {
            return ToInventoryWeapon(ToInventoryItem(groundedItem, autoDestroyGroundedItem));
        }

        public static InventoryWeapon ToInventoryWeapon(InventoryItem inventoryItem)
        {
            return new InventoryWeapon(inventoryItem.ItemData);
        }
        #endregion
    }
}
