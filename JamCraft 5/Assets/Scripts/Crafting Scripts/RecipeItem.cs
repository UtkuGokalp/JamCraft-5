using UnityEngine;
using JamCraft5.Items;

namespace JamCraft5.Crafting
{
    [System.Serializable]
    public class RecipeItem
    {
        [SerializeField]
        private int requiredItemCount;
        [SerializeField]
        private InventoryItem item;

        public int RequiredItemCount => requiredItemCount;
        public InventoryItem Item => item;
    }
}
