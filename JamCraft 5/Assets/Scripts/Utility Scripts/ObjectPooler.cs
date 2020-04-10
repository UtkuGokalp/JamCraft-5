using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Utility.Development
{
    public class ObjectPooler<T> where T : MonoBehaviour
    {
        #region Variables
        private T prefab;
        private Queue<T> pool;
        private List<T> objects;
        public bool HasObjects => pool.Count != 0;
        #endregion

        #region Constructor
        public ObjectPooler(T prefab, int capacity)
        {
            this.prefab = prefab;
            objects = new List<T>();
            pool = new Queue<T>(capacity);
            AddPrefabsToPool(capacity);
        }
        #endregion

        #region GetObject
        /// <summary>
        /// Gets the next object in the pool and returns it after enabling it. If there are no items left in the pool, grabs the oldest one in the scene.
        /// </summary>
        public T GetObject()
        {
            if (!HasObjects)
            {
                PutObject(objects[0]);
                objects.RemoveAt(0);
            }
            T obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
            objects.Add(obj);
            return obj;
        }
        #endregion

        #region PutObject
        /// <summary>
        /// Puts a new object to the pool and disables it.
        /// </summary>
        public void PutObject(T item)
        {
            pool.Enqueue(item);
            item.gameObject.SetActive(false);
        }
        #endregion

        #region PutObjectAfter
        /// <summary>
        /// Puts the object to the pool after given time.
        /// </summary>
        public void PutObjectAfter(T item, float time)
        {
            //time + 1 because computer must be finished putting the object back to pool and then destroy the MonoBehaviourHelper
            MonoBehaviourHelper.CreateTemporaryMonoBehaviour(time + 1).StartCoroutine(ObjectLifeControllerCoroutine(item, time));
        }
        #endregion

        #region AddPrefabsToPool
        private void AddPrefabsToPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                T obj = Object.Instantiate(prefab, Vector3.zero, prefab.transform.rotation);
                obj.gameObject.SetActive(false);
                pool.Enqueue(obj);
            }
        }
        #endregion

        #region ObjectLifeControllerCoroutine
        private IEnumerator ObjectLifeControllerCoroutine(T item, float time)
        {
            yield return new WaitForSeconds(time);
            PutObject(item);
        }
        #endregion
    }
}
