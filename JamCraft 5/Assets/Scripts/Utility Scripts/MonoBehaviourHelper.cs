using UnityEngine;

namespace Utility.Development
{
    public class MonoBehaviourHelper : MonoBehaviour
    {
        #region CreateTemporaryMonoBehaviour
        /// <summary>
        /// Creates a new MonoBehaviourHelper object and returns it in order for the client to use.
        /// </summary>
        public static MonoBehaviourHelper CreateTemporaryMonoBehaviour(float? lifeTime, string name = null)
        {
            GameObject go = new GameObject(name ?? "Mono Behaviour Helper", typeof(MonoBehaviourHelper));
            MonoBehaviourHelper monoBehaviourHelper = go.GetComponent<MonoBehaviourHelper>();
            if (lifeTime != null)
            {
                Destroy(go, (float)lifeTime);
            }
            return monoBehaviourHelper;
        }
        #endregion
    }
}
