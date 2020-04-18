using UnityEngine;
using System.Reflection;

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
        } 
        #endregion
    }
}
