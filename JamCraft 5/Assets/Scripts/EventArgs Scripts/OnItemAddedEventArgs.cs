using JamCraft5.Items;
using System;

namespace JamCraft5.EventArguments
{
    public class OnItemAddedEventArgs : EventArgs
    {
        #region Variables
        public InventoryItem AddedItem { get; }
        #endregion

        #region Constructor
        public OnItemAddedEventArgs(InventoryItem addedItem)
        {
            AddedItem = addedItem;
        }
        #endregion
    }
}
