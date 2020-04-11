using UnityEngine;

namespace JamCraft5.Enemies
{
    public class EnemyLoot : MonoBehaviour
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
