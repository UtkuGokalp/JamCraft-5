using UnityEngine;
using JamCraft5.Items;
using JamCraft5.Inventory;
using System.Collections.Generic;
using JamCraft5.EventArguments;

namespace JamCraft5.Player.Inventory
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private InventoryItem[] startingItems;
        [SerializeField]
        private InventoryWeapon[] startingWeapons;
        private List<InventorySlot> inventory;
        private int currentWeaponSlotIndex;

        public List<InventorySlot> Inventory
        {
            get
            {
                if (inventory == null)
                {
                    inventory = new List<InventorySlot>();
                }
                return inventory;
            }
        }
        private InventoryWeaponSlot[] weapons;
        public InventoryWeaponSlot SelectedWeapon => Weapons[CurrentWeaponSlotIndex];
        public int CurrentWeaponSlotIndex
        {
            get => currentWeaponSlotIndex;
            private set
            {
                if (value >= Weapons.Length)
                {
                    value = 0;
                }
                else if (value < 0)
                {
                    value = Weapons.Length - 1;
                }

                currentWeaponSlotIndex = value;
                OnSelectedWeaponChanged?.Invoke(this, new OnSelectedWeaponChangedEventArgs(currentWeaponSlotIndex, Weapons[currentWeaponSlotIndex].ContainedWeapon));
            }
        }
        public InventoryWeaponSlot[] Weapons
        {
            get
            {
                if (weapons == null)
                {
                    const int MAX_WEAPON_COUNT = 4;
                    weapons = new InventoryWeaponSlot[MAX_WEAPON_COUNT];

                    for (int i = 0; i < MAX_WEAPON_COUNT; i++)
                    {
                        weapons[i] = new InventoryWeaponSlot();
                    }
                }
                return weapons;
            }
        }
        public event System.EventHandler<OnItemAddedEventArgs> OnItemAdded;
        public event System.EventHandler<OnItemRemovedEventArgs> OnItemRemoved;
        public event System.EventHandler<OnWeaponAddedEventArgs> OnWeaponAdded;
        public event System.EventHandler<OnWeaponRemovedEventArgs> OnWeaponRemoved;
        public event System.EventHandler<OnSelectedWeaponChangedEventArgs> OnSelectedWeaponChanged;
        #endregion

        #region Start
        private void Start()
        {
            foreach (InventoryItem item in startingItems)
            {
                AddItem(item);
            }

            foreach (InventoryWeapon weapon in startingWeapons)
            {
                AddWeapon(weapon);
            }
        }
        #endregion

        #region Update
        private void Update()
        {
            if (!Attack.PlayerAttack.Attacking)
            {
                //Substracting because mouseScrollDelta is positive when we scroll up but we want
                //the selection to go up in game (visually), which means decreasing the index.
                CurrentWeaponSlotIndex -= (int)Input.mouseScrollDelta.y;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                CurrentWeaponSlotIndex = 0;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                CurrentWeaponSlotIndex = 1;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                CurrentWeaponSlotIndex = 2;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                CurrentWeaponSlotIndex = 3;
            }
        }
        #endregion

        #region AddItem
        /// <summary>
        /// Adds the given item to inventory if possible. Returns false otherwise.
        /// </summary>
        /// <returns></returns>
        public (bool added, InventoryItem addedItem) AddItem(InventoryItem item)
        {
            var inventoryItemData = HasItem(item);
            if (inventoryItemData.contains)
            {
                inventoryItemData.slotContained.ItemCount++;
                OnItemAdded?.Invoke(this, new OnItemAddedEventArgs(item));
                return System.ValueTuple.Create(true, item);
            }
            else
            {
                Inventory.Add(new InventorySlot());
                InventorySlot lastSlot = Inventory[Inventory.Count - 1];
                lastSlot.ContainedItem = item;
                lastSlot.ItemCount = 1;
                OnItemAdded?.Invoke(this, new OnItemAddedEventArgs(item));
                return System.ValueTuple.Create(true, item);
            }
        }
        #endregion

        #region RemoveItem
        /// <summary>
        /// Tries to remove item from inventory. Returns false if item does not exist or there are less items in the inventory than requested number.
        /// </summary>
        public (bool removed, InventoryItem removedItem) RemoveItem(InventoryItem item, int itemsToRemove)
        {
            var inventoryItemData = HasItem(item);
            if (itemsToRemove < 0)
            {
                throw new System.ArgumentOutOfRangeException("Items to remove can not be less than zero.");
            }
            if (inventoryItemData.contains && inventoryItemData.slotContained.ItemCount >= itemsToRemove)
            {
                inventoryItemData.slotContained.ItemCount -= itemsToRemove;
                if (inventoryItemData.slotContained.ItemCount == 0)
                {
                    Inventory.Remove(inventoryItemData.slotContained);
                }
                OnItemRemoved?.Invoke(this, new OnItemRemovedEventArgs(item));
                return System.ValueTuple.Create(true, item);
            }
            return System.ValueTuple.Create<bool, InventoryItem>(false, null);
        }
        #endregion

        #region AddWeapon
        public (bool added, InventoryWeapon addedWeapon) AddWeapon(InventoryWeapon weapon)
        {
            if (!HasWeapon(weapon).contains)
            {
                for (int i = 0; i < Weapons.Length; i++)
                {
                    if (Weapons[i].ContainedWeapon == null)
                    {
                        Weapons[i].ContainedWeapon = weapon;
                        OnWeaponAdded?.Invoke(this, new OnWeaponAddedEventArgs(i, weapon));
                        return System.ValueTuple.Create(true, weapon);
                    }
                }
            }
            return System.ValueTuple.Create<bool, InventoryWeapon>(false, null);
        }
        #endregion

        #region RemoveWeapon
        public (bool added, InventoryWeapon removedWeapon) RemoveWeapon(InventoryWeapon weapon)
        {
            for (int i = 0; i < Weapons.Length; i++)
            {
                if (Weapons[i].ContainedWeapon == weapon)
                {
                    Weapons[i].ContainedWeapon = null;
                    OnWeaponRemoved?.Invoke(this, new OnWeaponRemovedEventArgs(i, weapon));
                    return System.ValueTuple.Create(true, weapon);
                }
            }
            return System.ValueTuple.Create<bool, InventoryWeapon>(false, null);
        }
        #endregion

        #region HasItem
        public (bool contains, InventorySlot slotContained) HasItem(InventoryItem item)
        {
            foreach (InventorySlot slot in Inventory)
            {
                //In editor, if inspector is set to debug mode and if the contained item is expected to be null
                //this will give an error because slot.ContainedWeapon will not be null. Otherwise, this code works fine.
                if (slot.ContainedItem == null)
                {
                    continue;
                }
                if (slot.ContainedItem.ItemData.ID == item.ItemData.ID)
                {
                    return System.ValueTuple.Create(true, slot);
                }
            }
            return System.ValueTuple.Create<bool, InventorySlot>(false, null);
        }
        #endregion

        #region HasWeapon
        public (bool contains, InventoryWeaponSlot slotContained) HasWeapon(InventoryWeapon weapon)
        {
            //In editor, if inspector is set to debug mode, this will give an error
            //because slot.ContainedWeapon will not be null. Otherwise, this code works fine.
            foreach (InventoryWeaponSlot slot in Weapons)
            {
                if (slot.ContainedWeapon == null)
                {
                    continue;
                }
                if (slot.ContainedWeapon.ItemData.ID == weapon.ItemData.ID)
                {
                    return System.ValueTuple.Create(true, slot);
                }
            }
            return System.ValueTuple.Create<bool, InventoryWeaponSlot>(false, null);
        }
        #endregion
    }
}
