using System;
using JamCraft5.Items;

namespace JamCraft5.EventArguments
{
    public class OnItemRemovedEventArgs : EventArgs
    {
        #region Variables
        public InventoryItem RemovedItem { get; }
        #endregion

        #region Constructor
        public OnItemRemovedEventArgs(InventoryItem removedItem)
        {
            RemovedItem = removedItem;
        }
        #endregion
    }
}
