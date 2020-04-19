using UnityEngine;
using System.Collections.Generic;

namespace JamCraft5.Items.Controllers
{
    public class GroundedItemDropController : MonoBehaviour
    {
        #region Variables
        [Header("Possible Items")]
        [SerializeField]
        private int minPossibleItemsCount;
        [SerializeField]
        private int maxPossibleItemsCount;
        [SerializeField]
        private GroundedItem[] possibleItems;
  
        [Header("Definite Items")]
        [SerializeField]
        private int minDefiniteItemsCount;
        [SerializeField]
        private int maxDefiniteItemsCount;
        [SerializeField]
        private GroundedItem[] definiteItems;
        private Transform transformCache;
  
        /// <summary>
        /// If we have 4 possible items with %70, %50, %30, %10 drop chance respectively
        /// this variable will be set to the max, in this case %70.
        /// </summary>
        private int maxPossibleChance;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            foreach (GroundedItem item in possibleItems)
            {
                if (item.DropChance > maxPossibleChance)
                {
                    maxPossibleChance = item.DropChance;
                }
            }
        }
        #endregion

        #region DropItems
        public void DropItems()
        {
            //Calculate and drop possible items.
            if (this.possibleItems.Length == 0)
            {
                goto dropDefiniteItems;
            }
            int itemChance = Random.Range(0, maxPossibleChance);
            List<GroundedItem> possibleItems = new List<GroundedItem>();
            foreach (GroundedItem item in this.possibleItems)
            {
                if (item.DropChance > itemChance)
                {
                    possibleItems.Add(item);
                }
            }

            int possibleItemCount = Random.Range(minPossibleItemsCount, maxPossibleItemsCount);
            for (int i = 0; i < possibleItemCount; i++)
            {
                GroundedItem randomPossibleItem = possibleItems[Random.Range(0, possibleItems.Count)];
                Instantiate(randomPossibleItem, transformCache.position, randomPossibleItem.TransformCache.rotation);
            }

            dropDefiniteItems:
            //Drop definite items.
            if (definiteItems.Length == 0)
            {
                return;
            }
            int definiteItemCount = Random.Range(minDefiniteItemsCount, maxDefiniteItemsCount);
            for (int i = 0; i < definiteItemCount; i++)
            {
                GroundedItem randomDefiniteItem = definiteItems[Random.Range(0, definiteItems.Length)];
                Instantiate(randomDefiniteItem, transformCache.position, randomDefiniteItem.TransformCache.rotation);
            }
        }
        #endregion

        #region OnValidate
        private void OnValidate()
        {
            if (minPossibleItemsCount < 0)
            {
                minPossibleItemsCount = 0;
            }
            if (minDefiniteItemsCount < 0)
            {
                minDefiniteItemsCount = 0;
            }

            if (minPossibleItemsCount > maxPossibleItemsCount)
            {
                maxPossibleItemsCount = minPossibleItemsCount;
            }
            if (maxPossibleItemsCount < minPossibleItemsCount)
            {
                minPossibleItemsCount = maxPossibleItemsCount;
            }

            if (minDefiniteItemsCount > maxDefiniteItemsCount)
            {
                maxDefiniteItemsCount = minDefiniteItemsCount;
            }
            if (maxDefiniteItemsCount < minDefiniteItemsCount)
            {
                minDefiniteItemsCount = maxDefiniteItemsCount;
            }
        }
        #endregion
    }
}
