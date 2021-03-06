﻿using UnityEngine;
using JamCraft5.Items;
using JamCraft5.Player.Inventory;

namespace JamCraft5.Crafting
{
    [CreateAssetMenu(menuName = "Crafting/New Craftable")]
    public class Craftable : ScriptableObject
    {
        #region Variables
        [SerializeField]
        private Recipe recipe;
        public Recipe Recipe => recipe;
        #endregion

        #region Craft
        /// <summary>
        /// Checks if this item can be crafted and returns crafted item if possible. Calling CanCraft for checking manually is not recommended since it will be relatively expensive.
        /// </summary>
        public InventoryItem Craft(PlayerInventoryManager playerInventory)
        {
            if (CanCraft(playerInventory))
            {
                foreach (RecipeItem item in recipe.RequiredItems)
                {
                    if (item.Item.ItemData.type == 0) //If weapon
                    {
                        playerInventory.RemoveWeapon(ItemConverter.ToInventoryWeapon(item.Item));
                    }
                    else
                    {
                        playerInventory.RemoveItem(item.Item, item.RequiredItemCount);
                    }
                }
                return recipe.OutputItem;
            }
            return null;
        }
        #endregion

        #region CanCraft
        /// <summary>
        /// Checks if this item can be crafted.
        /// </summary>
        public bool CanCraft(PlayerInventoryManager playerInventory)
        {
            if (recipe.OutputItem.ItemData.type == 0) //If output item is a weapon
            {
                if (playerInventory.HasWeapon(ItemConverter.ToInventoryWeapon(recipe.OutputItem)).contains)
                {
                    return false;
                }

                foreach (RecipeItem item in recipe.RequiredItems)
                {
                    if (item.Item.ItemData.type == 0) // If weapon
                    {
                        var weaponData = playerInventory.HasWeapon(ItemConverter.ToInventoryWeapon(item.Item));
                        if (!weaponData.contains || weaponData.slotContained.ItemCount < item.RequiredItemCount)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        var itemData = playerInventory.HasItem(item.Item);
                        if (!itemData.contains || itemData.slotContained.ItemCount < item.RequiredItemCount)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            else
            {
                foreach (RecipeItem item in recipe.RequiredItems)
                {
                    var itemData = playerInventory.HasItem(item.Item);
                    if (!itemData.contains || itemData.slotContained.ItemCount < item.RequiredItemCount)
                    {
                        return false;
                    }
                }
                return true;
            }
        }
        #endregion
    }
}
