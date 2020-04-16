using UnityEngine;
using JamCraft5.Items;
using System.Collections.Generic;

namespace JamCraft5.Crafting
{
    [CreateAssetMenu(menuName = "Crafting/New Recipe")]
    public class Recipe : ScriptableObject
    {
        #region Variables
        [SerializeField]
        private List<RecipeItem> requiredItems;
        [SerializeField]
        private InventoryItem outputItem;

        public RecipeItem[] RequiredItems => requiredItems.ToArray();
        public InventoryItem OutputItem => outputItem;
        #endregion

        #region OnValidate
        private void OnValidate()
        {
            foreach (RecipeItem item in requiredItems)
            {
                if (item.RequiredItemCount < 1)
                {
                    typeof(RecipeItem).GetField("requiredItemCount", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(item, 1);
                }
            }

            if (requiredItems.Count == 0)
            {
                return;
            }
            List<RecipeItem> t = requiredItems.GetRange(0, requiredItems.Count - 1);
            RecipeItem lastItem = requiredItems[requiredItems.Count - 1];

            if (lastItem.Item.ItemData == null)
            {
                return;
            }

            foreach (RecipeItem item in t)
            {
                if (lastItem.Item.ItemData.ID == item.Item.ItemData.ID)
                {
                    lastItem.Item.ItemData = null;
                    Debug.LogWarning("All recipe items in requiredItems list must be unique.");
                    break;
                }
            }
        }
        #endregion
    }
}
