using UnityEngine;
using JamCraft5.Items;

namespace JamCraft5.Inventory
{
    [System.Serializable]
    public class InventoryWeaponSlot
    {
        [SerializeField]
        private InventoryWeapon containedWeapon;
        public InventoryWeapon ContainedWeapon
        {
            get => containedWeapon;
            set => containedWeapon = value;
        }
        public int ItemCount => ContainedWeapon == null ? 0 : 1;
    }
}
