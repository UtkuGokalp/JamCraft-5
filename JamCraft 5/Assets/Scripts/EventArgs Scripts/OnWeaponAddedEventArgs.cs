using JamCraft5.Items;
using System;

namespace JamCraft5.EventArguments
{
    public class OnWeaponAddedEventArgs : EventArgs
    {
        #region Variables
        public int SlotIndex { get; }
        public InventoryWeapon AddedWeapon { get; }
        #endregion

        #region Constructor
        public OnWeaponAddedEventArgs(int slotIndex, InventoryWeapon addedWeapon)
        {
            SlotIndex = slotIndex;
            AddedWeapon = addedWeapon;
        }
        #endregion
    }
}
