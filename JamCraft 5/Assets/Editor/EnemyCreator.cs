using UnityEngine;
using UnityEditor;
using JamCraft5.Enemies;
using JamCraft5.Items.Controllers;
using JamCraft5.Enemies.Components;

namespace JamCraft5.Editor
{
    public class EnemyCreator : EditorWindow
    {
        #region EnemyType
        private enum EnemyType
        {
            Grey,
            Blue,
            Red
        }
        #endregion

        #region Variables
        private EnemyType enemyType;
        private GameObject enemyPrefab;
        private GameObject lastFrameEnemyPrefab;
        #endregion

        #region OpenWindow
        [MenuItem("Tools/Enemy Creator")]
        public static void OpenWindow()
        {
            GetWindow<EnemyCreator>("Enemy Creator");
        }
        #endregion

        #region OnGUI
        private void OnGUI()
        {
            enemyPrefab = (GameObject)EditorGUILayout.ObjectField("Enemy Prefab", enemyPrefab, typeof(GameObject), false);

            if (lastFrameEnemyPrefab == null && enemyPrefab != null)
            {
                string prefabName = enemyPrefab.name.ToLower();

                if (prefabName.Contains("grey"))
                {
                    enemyType = EnemyType.Grey;
                }
                else if (prefabName.Contains("blue"))
                {
                    enemyType = EnemyType.Blue;
                }
                else if (prefabName.Contains("red"))
                {
                    enemyType = EnemyType.Red;
                }
            }

            lastFrameEnemyPrefab = enemyPrefab;

            enemyType = (EnemyType)EditorGUILayout.EnumPopup("Enemy Type", enemyType);

            if (GUILayout.Button("Add Components"))
            {
                if (enemyPrefab == null)
                {
                    Debug.LogWarning("Assign an enemy prefab first.");
                    return;
                }

                Rigidbody rb = AddComponentIfDoesntExist<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezeRotation;
                rb.mass = 100;

                //This piece is implemented manually rather than using AddComponentIfDoesntExist method
                //because we're checking for a Collider component and if we don't find one, we add
                //a CapsuleCollider component as a default one.
                if (enemyPrefab.GetComponent<Collider>() == null)
                {
                    CapsuleCollider collider = enemyPrefab.AddComponent<CapsuleCollider>();
                    collider.radius = 0.5f;
                    collider.height = 2;
                    collider.direction = 1;
                }

                AddComponentIfDoesntExist<GroundedItemDropController>();
                AddComponentIfDoesntExist<EnemyState>();
                AddComponentIfDoesntExist<EnemyChasePlayerComponent>();

                switch (enemyType)
                {
                    case EnemyType.Grey:
                        AddComponentIfDoesntExist<EnemyMeleeAttackComponent>();
                        break;
                    case EnemyType.Blue:
                        AddComponentIfDoesntExist<EnemyRangedAttackComponent>();
                        break;
                    case EnemyType.Red:
                        AddComponentIfDoesntExist<EnemyMeleeAttackComponent>();
                        break;
                }

                Debug.Log("Done.");

                enemyPrefab = null;
                enemyType = default;
            }
        }
        #endregion

        #region AddComponentIfDoesntExist<T>
        private T AddComponentIfDoesntExist<T>() where T : Component
        {
            T component = enemyPrefab.GetComponent<T>();
            if (component == null)
            {
                component = enemyPrefab.AddComponent<T>();
            }
            return component;
        }
        #endregion
    }
}
