using UnityEngine;

namespace JamCraft5.Enemies
{
    [DisallowMultipleComponent]
    public class EnemyState : MonoBehaviour
    {
        //There is a class instead of a simple enum because all enemies need a state
        //and just a simple enum can not be on a gameobject.
        public EnemyStateEnum StateOfEnemy { get; set; }
    }
}
