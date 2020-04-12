using UnityEngine;
using JamCraft5.Items;

namespace JamCraft5.Player.Inventory
{
    [RequireComponent(typeof(PlayerInventoryManager))]
    public class PlayerItemDropController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private InventoryItem removeItem;
        private PlayerInventoryManager playerInventoryManager;
        #endregion

        #region Awake
        private void Awake()
        {
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }
        #endregion

        private void Update()
        {
            if (Input.GetMouseButtonDown(Utility.Development.MouseButton.MIDDLE))
            {
                var removedItemData = playerInventoryManager.RemoveItem(removeItem, 1);
                if (removedItemData.removed)
                {
                    ItemConverter.ToGroundedItem(transform.position, removedItemData.removedItem);
                }
            }
        }
    }
}
