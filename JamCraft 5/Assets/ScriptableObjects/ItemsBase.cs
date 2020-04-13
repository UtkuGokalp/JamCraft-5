using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemsBase : ScriptableObject
{
    public int ID { get; private set; }
    public string itemName;
    public int type;//0-weapon, 1-grenade, 2-material, 3-Healing

    public int weaponDamage;
    public int weaponRange;

    public int grenadeDamage;
    public int grenadeRange;
    public int grenadeForce;
    public float grenadeTime;

    public string matType;

    public int healForce;
}
