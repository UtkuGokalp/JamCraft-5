using UnityEngine;
using JamCraft5.Items;

namespace JamCraft5.Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        [SerializeField]
        private int itemCount;
        [SerializeField]
        private InventoryItem containedItem;
        public int ItemCount
        {
            get => itemCount;
            set => itemCount = value;
        }
        public InventoryItem ContainedItem
        {
            get => containedItem;
            set => containedItem = value;
        }
    }
}
