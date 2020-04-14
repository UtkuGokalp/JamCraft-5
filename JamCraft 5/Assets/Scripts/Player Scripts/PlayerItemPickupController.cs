using UnityEngine;
using JamCraft5.Items;

namespace JamCraft5.Player.Inventory
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(PlayerInventoryManager))]
    public class PlayerItemPickupController : MonoBehaviour
    {
        #region Variables
        private PlayerInventoryManager playerInventoryManager;
        #endregion

        #region Awake
        private void Awake()
        {
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }
        #endregion

        #region OnCollisionEnter
        private void OnCollisionEnter(Collision collision)
        {
            GroundedItem groundedItem = collision.gameObject.GetComponent<GroundedItem>();
            if (groundedItem != null)
            {
                InventoryItem inventoryItem = ItemConverter.ToInventoryItem(groundedItem, true);
                playerInventoryManager.AddItem(inventoryItem);
            }
        }
        #endregion
    }
}
