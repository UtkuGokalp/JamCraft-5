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
                if (groundedItem.ItemData.type == 0) // If collected item is a weapon
                {
                    playerInventoryManager.AddWeapon(ItemConverter.ToInventoryWeapon(groundedItem));
                }
                else
                {
                    playerInventoryManager.AddItem(ItemConverter.ToInventoryItem(groundedItem, true));
                }
            }
        }
        #endregion
    }
}
