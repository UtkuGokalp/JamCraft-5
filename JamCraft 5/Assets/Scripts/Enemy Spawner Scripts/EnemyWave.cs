using UnityEngine;

namespace JamCraft5.Enemies.Spawning
{
    [System.Serializable]
    public class EnemyWave
    {
        #region Variables
        [SerializeField]
        private int minEnemyCount;
        [SerializeField]
        private int maxEnemyCount;
        [SerializeField]
        private float enemySpawnRate;
        /// <summary>
        /// This is of type EnemyState because all the enemies need to have an EnemyState script attached to them.
        /// </summary>
        [SerializeField]
        private EnemyState[] enemyPrefabs;
        public float EnemySpawnRate => enemySpawnRate;
        public int MinEnemyCount => minEnemyCount;
        public int MaxEnemyCount => maxEnemyCount;
        #endregion

        #region SpawnRandomEnemy
        public EnemyState SpawnRandomEnemy(Vector3 spawnPosition)
        {
            EnemyState enemyToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            return Object.Instantiate(enemyToSpawn, spawnPosition, enemyToSpawn.transform.rotation);
        }
        #endregion
    }
}
