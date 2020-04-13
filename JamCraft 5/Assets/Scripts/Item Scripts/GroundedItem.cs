using UnityEngine;
using Utility.Development;

namespace JamCraft5.Items
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    public class GroundedItem : MonoBehaviour
    {
        #region Variables
        [Range(0, 100)]
        [SerializeField]
        private int dropChance;
        [SerializeField]
        private ItemsBase itemData;
        private Transform transformCache;
        private Collider colliderComponent;
        private Rigidbody rigidbodyComponent;
        public int DropChance => dropChance;
        public ItemsBase ItemData
        {
            get => itemData;
            private set => itemData = value;
        }
        public Transform TransformCache
        {
            get
            {
                if (transformCache == null)
                {
                    transformCache = transform;
                }
                return transformCache;
            }
        }
        public Collider ColliderComponent
        {
            get
            {
                if (colliderComponent == null)
                {
                    colliderComponent = GetComponent<Collider>();
                }
                return colliderComponent;
            }
        }
        public Rigidbody RigidbodyComponent
        {
            get
            {
                if (rigidbodyComponent == null)
                {
                    rigidbodyComponent = GetComponent<Rigidbody>();
                }
                return rigidbodyComponent;
            }
        }
        #endregion

        #region Awake
        private void Awake()
        {
            gameObject.layer = GameUtility.GroundedItemLayer;
        }
        #endregion
    }
}
