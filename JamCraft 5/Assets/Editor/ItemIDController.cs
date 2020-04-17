using UnityEngine;
using UnityEditor;
using System.Reflection;

namespace JamCraft5.Editor
{
    public class ItemIDController : UnityEditor.Editor
    {
        #region CreateUniqueIDs
        [MenuItem("Tools/Create Unique IDs")]
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
            Debug.Log("All items are set to have unique IDs.");
        } 
        #endregion
    }
}
