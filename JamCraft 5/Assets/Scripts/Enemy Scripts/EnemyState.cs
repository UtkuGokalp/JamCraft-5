using UnityEngine;
using Utility.Development;

namespace JamCraft5.Enemies
{
    //There is a class instead of a simple enum because all enemies need a state
    //and just a simple enum can not be on a gameobject.
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    public class EnemyState : MonoBehaviour
    {
        #region Variables
        public EnemyStateEnum StateOfEnemy { get; set; }
        #endregion
    }
}
