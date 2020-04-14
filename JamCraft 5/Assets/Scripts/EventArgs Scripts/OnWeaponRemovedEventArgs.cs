using System;
using JamCraft5.Items;

namespace JamCraft5.EventArguments
{
    public class OnWeaponRemovedEventArgs : EventArgs
    {
        #region Variables
        public int SlotIndex { get; }
        public InventoryWeapon RemovedWeapon { get; }
        #endregion

        #region Constructor
        public OnWeaponRemovedEventArgs(int slotIndex, InventoryWeapon removedWeapon)
        {
            SlotIndex = slotIndex;
            RemovedWeapon = removedWeapon;
        }
        #endregion
    }
}
