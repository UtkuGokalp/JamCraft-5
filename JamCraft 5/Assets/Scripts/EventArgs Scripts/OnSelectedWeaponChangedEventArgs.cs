using JamCraft5.Items;
using System;

namespace JamCraft5.EventArguments
{
    public class OnSelectedWeaponChangedEventArgs : EventArgs
    {
        #region Variables
        public int SlotIndex { get; }
        public InventoryWeapon CurrentWeapon { get; }
        #endregion

        #region Constructor
        public OnSelectedWeaponChangedEventArgs(int slotIndex, InventoryWeapon currentWeapon)
        {
            SlotIndex = slotIndex;
            CurrentWeapon = currentWeapon;
        }
        #endregion
    }
}
