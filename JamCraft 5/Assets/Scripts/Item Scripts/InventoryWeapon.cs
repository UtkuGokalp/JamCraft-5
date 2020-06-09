using UnityEngine;
using JamCraft5.Weapons;
using JamCraft5.Player.Inventory;

namespace JamCraft5.Items
{
    [System.Serializable]
    public class InventoryWeapon : InventoryItem
    {
        #region Variables
        private static PlayerInventoryManager inventoryManager;
        public static WeaponType CurrentWeaponType
        {
            get
            {
                if (inventoryManager == null)
                {
                    inventoryManager = Object.FindObjectOfType<PlayerInventoryManager>();
                }
                return (WeaponType)inventoryManager.CurrentWeaponSlotIndex;
            }
        }
        #endregion

        #region Constructor
        public InventoryWeapon(ItemsBase itemData) : base(itemData)
        {

        }
        #endregion
    }
}
