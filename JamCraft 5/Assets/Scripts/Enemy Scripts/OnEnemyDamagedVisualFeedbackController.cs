using UnityEngine;
using Utility.Health;
using System.Collections.Generic;
using System.Collections;

namespace JamCraft5.Enemies.Visuals
{
    [RequireComponent(typeof(HealthSystem))]
    public class OnEnemyDamagedVisualFeedbackController : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private float feedbackTime = 0.1f;
        [SerializeField]
        private Color damagedColor = Color.red;
        private HealthSystem healthSystem;
        private SkinnedMeshRenderer[] enemyRenderers;
        private Material[] rendererMaterials;
        private bool changingColor;
        #endregion

        #region Awake
        private void Awake()
        {
            healthSystem = GetComponent<HealthSystem>();
            //Get all the renderers(skinned mesh renderers) that renders this enemy
            enemyRenderers = transform.GetChild(0).GetComponentsInChildren<SkinnedMeshRenderer>();
            //Get all the materials in the renderers
            List<Material> rendererMaterials = new List<Material>();
            foreach (SkinnedMeshRenderer renderer in enemyRenderers)
            {
                for (int i = 0; i < renderer.materials.Length; i++)
                {
                    rendererMaterials.Add(renderer.materials[i]);
                }
            }
            this.rendererMaterials = rendererMaterials.ToArray();
        }
        #endregion

        #region OnEnable
        private void OnEnable()
        {
            healthSystem.OnDeath += OnDeath;
            healthSystem.OnHealthChanged += OnHealthChanged;
        }
        #endregion

        #region OnDisable
        private void OnDisable()
        {
            healthSystem.OnDeath -= OnDeath;
            healthSystem.OnHealthChanged -= OnHealthChanged;
        }
        #endregion

        #region OnDeath
        private void OnDeath()
        {
            ChangeEnemyColor();
        }
        #endregion
        
        #region OnHealthChanged
        private void OnHealthChanged(OnHealthChangedEventArgs e)
        {
            if (e.HealthChangeData == OnHealthChangedEventArgs.HealthChange.Decrease)
            {
                ChangeEnemyColor();
            }
        }
        #endregion

        #region ChangeEnemyColor
        /// <summary>
        /// Changes the enemy color to the damaged color and then changes it back.
        /// </summary>
        private void ChangeEnemyColor()
        {
            if (!changingColor)
            {
                changingColor = true;
                StartCoroutine(ChangeEnemyColorCoroutine());
            }
        }
        #endregion

        #region ChangeEnemyColorCoroutine
        /// <summary>
        /// Coroutine for changing the enemy color to the damaged color and then changing it back.
        /// </summary>
        private IEnumerator ChangeEnemyColorCoroutine()
        {
            Color[] oldColors = new Color[rendererMaterials.Length];

            for (int i = 0; i < oldColors.Length; i++)
            {
                oldColors[i] = rendererMaterials[i].color;
            }

            float currentTime = 0;

            while (currentTime / feedbackTime < 1)
            {
                foreach (Material material in rendererMaterials)
                {
                    material.color = Color.Lerp(material.color, damagedColor, currentTime / feedbackTime);
                }
                yield return null;
                currentTime += Time.deltaTime;
            }

            currentTime = 0; //Reset current time to lerp current colors to old colors

            while (currentTime / feedbackTime < 1)
            {
                for (int i = 0; i < rendererMaterials.Length; i++)
                {
                    rendererMaterials[i].color = Color.Lerp(rendererMaterials[i].color, oldColors[i], currentTime / feedbackTime);
                }
                currentTime += Time.deltaTime;
            }

            changingColor = false;
        }
        #endregion
    }
}
