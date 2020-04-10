using System;

namespace Utility.Health
{
    public class OnHealthChangedEventArgs : EventArgs
    {
        #region Variables
        public enum HealthChange { None, Increase, Decrease }
        public int HealthLeft { get; }
        public HealthChange HealthChangeData { get; }
        #endregion

        #region Constructor
        public OnHealthChangedEventArgs(int healthLeft, HealthChange healthChangeData)
        {
            HealthLeft = healthLeft;
            HealthChangeData = healthChangeData;
        }
        #endregion
    }
}
