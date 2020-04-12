using UnityEngine;
using System.Collections.Generic;

namespace JamCraft5.Enemies.Components
{
    public class EnemyLootComponent : MonoBehaviour
    {
        #region Variables
        [Header("Possible Loots")]
        [SerializeField]
        private int minPossibleLootCount;
        [SerializeField]
        private int maxPossibleLootCount;
        [SerializeField]
        private EnemyLoot[] possibleLoots;

        [Header("Definite Loots")]
        [SerializeField]
        private int minDefiniteLootCount;
        [SerializeField]
        private int maxDefiniteLootCount;
        [SerializeField]
        private EnemyLoot[] definiteLoots;
        private Transform transformCache;

        /// <summary>
        /// If we have 4 possible loots with %70, %50, %30, %10 drop chance respectively
        /// this variable will be set to the max, in this case %70.
        /// </summary>
        private int maxPossibleChance;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            foreach (EnemyLoot loot in possibleLoots)
            {
                if (loot.DropChance > maxPossibleChance)
                {
                    maxPossibleChance = loot.DropChance;
                }
            }
        }
        #endregion

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                DropLoots();
            }
        }

        #region DropLoots
        public void DropLoots()
        {
            //Calculate and drop possible loots.
            int lootChance = Random.Range(0, maxPossibleChance);
            List<EnemyLoot> possibleLoots = new List<EnemyLoot>();
            foreach (EnemyLoot loot in this.possibleLoots)
            {
                if (loot.DropChance > lootChance)
                {
                    possibleLoots.Add(loot);
                }
            }

            int possibleLootCount = Random.Range(minPossibleLootCount, maxPossibleLootCount);
            for (int i = 0; i < possibleLootCount; i++)
            {
                EnemyLoot randomPossibleLoot = possibleLoots[Random.Range(0, possibleLoots.Count)];
                Instantiate(randomPossibleLoot, transformCache.position, randomPossibleLoot.TransformCache.rotation);
            }

            //Drop definite loots.
            int definiteLootCount = Random.Range(minDefiniteLootCount, maxDefiniteLootCount);
            for (int i = 0; i < definiteLootCount; i++)
            {
                EnemyLoot randomDefiniteLoot = definiteLoots[Random.Range(0, definiteLoots.Length)];
                Instantiate(randomDefiniteLoot, transformCache.position, randomDefiniteLoot.TransformCache.rotation);
            }
        }
        #endregion

        #region OnValidate
        private void OnValidate()
        {
            if (minPossibleLootCount < 0)
            {
                minPossibleLootCount = 0;
            }
            if (minDefiniteLootCount < 0)
            {
                minDefiniteLootCount = 0;
            }

            if (minPossibleLootCount > maxPossibleLootCount)
            {
                maxPossibleLootCount = minPossibleLootCount;
            }
            if (maxPossibleLootCount < minPossibleLootCount)
            {
                minPossibleLootCount = maxPossibleLootCount;
            }

            if (minDefiniteLootCount > maxDefiniteLootCount)
            {
                maxDefiniteLootCount = minDefiniteLootCount;
            }
            if (maxDefiniteLootCount < minDefiniteLootCount)
            {
                minDefiniteLootCount = maxDefiniteLootCount;
            }
        }
        #endregion
    }
}
