using UnityEngine;
using System.Reflection;
using System.Collections.Generic;

namespace JamCraft5.Items.Controllers
{
    public class ItemIDController : MonoBehaviour
    {
        #region Awake
        private void Awake()
        {
            CreateUniqueIDs();
        }
        #endregion

        #region CreateUniqueIDs
        [ContextMenu("Create Unique IDs")]
        [UnityEditor.MenuItem("Tools/Create Unique IDs")]
        public static void CreateUniqueIDs()
        {
            ItemsBase[] items = Resources.LoadAll<ItemsBase>("Items");

            for (int i = 0; i < items.Length; i++)
            {
                ItemsBase currentItem = items[i];
                System.Type type = typeof(ItemsBase);
                PropertyInfo propertyInfo = type.GetProperty(nameof(currentItem.ID));
                propertyInfo.SetValue(currentItem, i);
            }

#if UNITY_EDITOR
            Debug.Log("All items now have unique IDs.");
#endif
        }
        #endregion

        #region AllItemsHaveUniqueIDs
        public static bool AllItemsHaveUniqueIDs()
        {
            ItemsBase[] items = Resources.LoadAll<ItemsBase>("Items");
            List<int> ids = new List<int>();

            foreach (ItemsBase item in items)
            {
                if (ids.Contains(item.ID))
                {
                    return false;
                }
                else
                {
                    ids.Add(item.ID);
                }
            }
            return true;
        }
        #endregion
    }
}
