using System;

namespace JamCraft5.EventArguments
{
    public class OnPlayerRotationChangedEventArgs : EventArgs
    {
        #region Variables
        /// <summary>
        /// Y rotation offset of player relative to the start of the game.
        /// </summary>
        public float PlayerRotationOffset { get; }
        #endregion

        #region Constructor
        public OnPlayerRotationChangedEventArgs(float playerRotationOffset)
        {
            PlayerRotationOffset = playerRotationOffset;
        }
        #endregion
    }
}
