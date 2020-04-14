using UnityEngine;

namespace JamCraft5.Items
{
    [System.Serializable]
    public class InventoryItem
    {
        #region Variables
        [SerializeField]
        private ItemsBase itemData;
        public ItemsBase ItemData
        {
            get => itemData;
            set => itemData = value;
        }
        #endregion
        
        #region Constructor
        public InventoryItem(ItemsBase itemData)
        {
            ItemData = itemData;
        }
        #endregion
    }
}
