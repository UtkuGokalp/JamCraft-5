using UnityEngine;
using UnityEditor;
using System.Reflection;
using JamCraft5.Utilities;

namespace JamCraft5.Editor
{
    [CustomEditor(typeof(ItemIDController))]
    public class ItemIDControllerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create Unique IDs"))
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
        }
    }
}
