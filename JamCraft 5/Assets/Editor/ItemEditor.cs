using UnityEngine;
using UnityEditor;
using JamCraft5.Items;

[CustomEditor(typeof(ItemsBase))]
public class ItemEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ItemsBase Items = (ItemsBase)target;

        Items.itemName = EditorGUILayout.TextField("Item name", Items.itemName);
        Items.groundedItemPrefab = (GroundedItem)EditorGUILayout.ObjectField("Grounded Item Prefab", Items.groundedItemPrefab, typeof(GroundedItem), true);

        Items.type = EditorGUILayout.Popup(Items.type, new string[] { "Weapons", "Grenade", "Materials", "Healing" });
        switch (Items.type)
        {
            case 0:
                Items.weaponDamage = EditorGUILayout.IntField("Damage", Items.weaponDamage);
                Items.weaponRange = EditorGUILayout.IntField("Range", Items.weaponRange);
                break;
            case 1:
                Items.grenadeDamage = EditorGUILayout.IntField("Damage", Items.grenadeDamage);
                Items.grenadeRange = EditorGUILayout.IntField("Range", Items.grenadeRange);
                Items.grenadeForce = EditorGUILayout.IntField("Force", Items.grenadeForce);
                Items.grenadeTime = EditorGUILayout.FloatField("Explosion time", Items.grenadeTime);
                break;
            case 2:
                Items.matType = EditorGUILayout.TextField("Material", Items.matType);
                break;
            case 3:
                Items.healForce = EditorGUILayout.IntField("Healing Force", Items.healForce);
                break;
        }
    }

}
