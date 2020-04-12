using UnityEngine;

namespace JamCraft5.Items
{
    public class GroundedItem : MonoBehaviour
    {
        #region Variables
        [Range(0, 100)]
        [SerializeField]
        private int dropChance;
        private Transform transformCache;
        public int DropChance => dropChance;
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
    }
}
