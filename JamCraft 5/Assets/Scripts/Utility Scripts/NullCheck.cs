using System;

namespace Utility.Development
{
    public class NullCheck<T> where T : class
    {
        #region Variables
        private T backField;
        private Func<T> getInstanceMethod;
        public T Value
        {
            get
            {
                if (backField == null)
                {
                    backField = getInstanceMethod();
                }
                return backField;
            }
        }
        #endregion

        #region Constructor
        public NullCheck(Func<T> getInstanceMethod)
        {
            this.getInstanceMethod = getInstanceMethod;
        }
        #endregion
    }
}
