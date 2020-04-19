using UnityEngine;
using UnityEditor;
using Utility.Health;
using System.Reflection;
using JamCraft5.Enemies;
using JamCraft5.Items.Controllers;
using JamCraft5.Enemies.Components;
using Utility.Development;

namespace JamCraft5.Editor
{
    public class EnemyCreator : EditorWindow
    {
        #region EnemyType
        private enum EnemyType
        {
            Grey,
            Blue,
            Red,
            Green
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
                else if (prefabName.Contains("green"))
                {
                    enemyType = EnemyType.Green;
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
                    collider.radius = 0.125f;
                    collider.height = 0.5f;
                    collider.direction = 1;
                    collider.center = Vector3.zero.With(null, 0.06f, null);
                }

                AddComponentIfDoesntExist<HealthSystem>();
                AddComponentIfDoesntExist<GroundedItemDropController>();
                AddComponentIfDoesntExist<EnemyState>();
                AddComponentIfDoesntExist<EnemyDamaged>();
                AddComponentIfDoesntExist<GroundedItemDropController>();

                var enemyChasePlayerComponent = AddComponentIfDoesntExist<EnemyChasePlayerComponent>();
                typeof(EnemyChasePlayerComponent).GetField("chaseSpeed", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(enemyChasePlayerComponent, 300);
                typeof(EnemyChasePlayerComponent).GetField("detectionRange", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(enemyChasePlayerComponent, 5);

                var enemyRotationComponent = AddComponentIfDoesntExist<EnemyRotationComponent>();
                typeof(EnemyRotationComponent).GetField("rotationSpeed", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(enemyRotationComponent, 5);


                EnemyAttackBaseComponent enemyAttackBase = null;
                switch (enemyType)
                {
                    case EnemyType.Grey:
                        //attackRange
                        //attackRate
                        enemyAttackBase = AddComponentIfDoesntExist<EnemyMeleeAttackComponent>();
                        break;
                    case EnemyType.Blue:
                        enemyAttackBase = AddComponentIfDoesntExist<EnemyRangedAttackComponent>();
                        break;
                    case EnemyType.Red:
                        enemyAttackBase = AddComponentIfDoesntExist<EnemyMeleeAttackComponent>();
                        break;
                    case EnemyType.Green:
                        enemyAttackBase = AddComponentIfDoesntExist<EnemyMeleeAttackComponent>();
                        break;
                }
                typeof(EnemyAttackBaseComponent).GetField("attackRange", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(enemyAttackBase, 2);

                AddComponentIfDoesntExist<EnemyAnimationController>();

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
