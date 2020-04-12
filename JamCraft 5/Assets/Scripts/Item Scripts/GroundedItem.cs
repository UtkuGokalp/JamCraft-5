using UnityEngine;

namespace JamCraft5.Items
{
    public class GroundedItem : MonoBehaviour
    {
        #region Variables
        [Range(0, 100)]
        [SerializeField]
        private int dropChance;
        [SerializeField]
        private ItemsBase itemData;
        private Transform transformCache;
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
        #endregion

        /// <summary>
        /// Instantiates a new grounded item at given position. (NOT COMPLETELY IMPLEMETED!)
        /// </summary>
        public static GroundedItem Create(Vector3 position, ItemsBase itemData)
        {
            GroundedItem groundedItem = new GameObject("Grounded Item", typeof(GroundedItem)).GetComponent<GroundedItem>();
            groundedItem.transform.position = position;
            groundedItem.ItemData = itemData;
            return groundedItem;
        }
    }
}
