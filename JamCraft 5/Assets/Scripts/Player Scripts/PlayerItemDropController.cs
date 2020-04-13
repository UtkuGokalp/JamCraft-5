using UnityEngine;
using JamCraft5.Items;
using Utility.Development;
using System.Collections;

namespace JamCraft5.Player.Inventory
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(PlayerInventoryManager))]
    public class PlayerItemDropController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float dropDistanceMultiplier;
        [SerializeField]
        private float throwForce;
        [SerializeField]
        private float collisionIgnoranceTime;
        [SerializeField]
        private InventoryItem removeItem; //TEMPORARY VARIABLE
        [SerializeField]
        private KeyCode dropKey = KeyCode.Q;
        private Transform transformCache;
        private Collider colliderComponent;
        private WaitForSeconds collisionIgnoranceTimeWaitForSeconds;
        private PlayerInventoryManager playerInventoryManager;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            collisionIgnoranceTimeWaitForSeconds = new WaitForSeconds(collisionIgnoranceTime);
            colliderComponent = GetComponent<Collider>();
            playerInventoryManager = GetComponent<PlayerInventoryManager>();
        }
        #endregion

        #region Update
        private void Update()
        {
            if (Input.GetKeyDown(dropKey))
            {
                DropItem();
            }
        }
        #endregion

        #region DropItem
        private void DropItem()
        {
            var removedItemData = playerInventoryManager.RemoveItem(removeItem, 1);
            if (removedItemData.removed)
            {
                Vector3 directionToMousePosition = GameUtility.GetDirectionToMouse(transformCache.position);
                Vector3 spawnPosition = transformCache.position + (directionToMousePosition * dropDistanceMultiplier);
                GroundedItem groundedItem = ItemConverter.ToGroundedItem(spawnPosition, removedItemData.removedItem);
                groundedItem.RigidbodyComponent.AddForce(directionToMousePosition * throwForce);
                StartCoroutine(DisableCollisionBetweenGroundedObjectAndPlayerCoroutine(groundedItem.ColliderComponent));
            }
        }
        #endregion

        #region DisableCollisionBetweenGroundedObjectAndPlayerCoroutine
        private IEnumerator DisableCollisionBetweenGroundedObjectAndPlayerCoroutine(Collider groundedItemCollider)
        {
            Physics.IgnoreCollision(groundedItemCollider, colliderComponent, true);
            yield return collisionIgnoranceTimeWaitForSeconds;
            Physics.IgnoreCollision(groundedItemCollider, colliderComponent, false);
        }
        #endregion
    }
}
