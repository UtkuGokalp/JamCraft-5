using UnityEngine;

namespace JamCraft5.Enemies.Spawning
{
    public class EnemySpawner : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float spawnRange;
        [SerializeField]
        private float timeBetweenWaves;
        [SerializeField]
        private EnemyWave[] waves;

        private bool spawning;
        private int currentWaveIndex;
        private int enemiesLeftForCurrentWave;
        private float currentTimeBetweenWaves;
        private float currentTimeUntilNextEnemy;

        private Transform transformCache;
        private EnemyWave CurrentWave => waves[currentWaveIndex];
        public bool Spawning
        {
            get
            {
                if (currentWaveIndex >= waves.Length)
                {
                    return false;
                }
                return spawning;
            }
            set => spawning = value;
        }
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
        }
        #endregion

        #region Update
        private void Update()
        {
#if UNITY_EDITOR
            //Test code
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!Spawning)
                {
                    StartSpawning();
                }
            }
#endif

            if (Spawning)
            {
                if (enemiesLeftForCurrentWave > 0)
                {
                    if (currentTimeUntilNextEnemy > 0)
                    {
                        currentTimeUntilNextEnemy -= Time.deltaTime;
                    }
                    else
                    {
                        CurrentWave.SpawnRandomEnemy(GetRandomSpawnPosition());
                        enemiesLeftForCurrentWave--;
                        currentTimeUntilNextEnemy = CurrentWave.EnemySpawnRate;
                    }
                }
                else
                {
                    if (currentTimeBetweenWaves > 0)
                    {
                        currentTimeBetweenWaves -= Time.deltaTime;
                    }
                    else
                    {
                        currentWaveIndex++;

                        if (currentWaveIndex >= waves.Length)
                        {
                            StopSpawning();
                            return;
                        }

                        enemiesLeftForCurrentWave = GetRandomEnemyCountForCurrentWave();
                        currentTimeBetweenWaves = timeBetweenWaves;
                    }
                }
            }
        }
        #endregion

         #region StartSpawning
        public void StartSpawning()
        {
            currentWaveIndex = 0;
            currentTimeBetweenWaves = timeBetweenWaves;
            currentTimeUntilNextEnemy = CurrentWave.EnemySpawnRate;
            enemiesLeftForCurrentWave = GetRandomEnemyCountForCurrentWave();
            Spawning = true;
        }
        #endregion

        #region StopSpawning
        public void StopSpawning()
        {
            Spawning = false;
        }
        #endregion

        #region GetRandomSpawnPosition
        private Vector3 GetRandomSpawnPosition() => transformCache.position + Random.insideUnitSphere * spawnRange;
        #endregion

        #region GetRandomEnemyCountForCurrentWave
        private int GetRandomEnemyCountForCurrentWave() => Random.Range(CurrentWave.MinEnemyCount, CurrentWave.MaxEnemyCount);
        #endregion

        #region OnValidate
        private void OnValidate()
        {
            if (spawnRange < 0)
            {
                spawnRange = 0;
            }
            if (timeBetweenWaves < 0)
            {
                timeBetweenWaves = 0;
            }
        }
        #endregion

        #region OnDrawGizmosSelected
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, spawnRange);
        }
        #endregion
    }
}
