using UnityEngine;
using Utility.Development;
using System.Collections.Generic;

namespace JamCraft5.Camera
{
    public class ObjectTransparencyController : MonoBehaviour
    {
        #region Variables
        private RaycastHit[] hitInfos;
        private List<Renderer> renderers;
        private List<Material> materials;
        private Transform transformCache;
        private const float ALPHA_CHANGE_SPEED = 1;
        private const float MAX_MATERIAL_ALPHA = 1;
        private const float MIN_MATERIAL_ALPHA = 0.25f;
        private bool everyMaterialHasMaxAlpha;
        private bool everyMaterialHasMinAlpha;
        #endregion

        #region Awake
        private void Awake()
        {
            transformCache = transform;
            hitInfos = new RaycastHit[10];
            renderers = new List<Renderer>();
            materials = new List<Material>();

            renderers.AddRange(GetComponents<Renderer>());
            renderers.AddRange(GetComponentsInChildren<Renderer>());
            foreach (Renderer renderer in renderers)
            {
                materials.AddRange(renderer.materials);
            }
            foreach (Material material in materials)
            {
                material.ChangeRenderMode(StandartShaderUtilities.BlendMode.Transparent);
            }
        }
        #endregion

        #region FixedUpdate
        private void FixedUpdate()
        {
            if (GameUtility.BetweenCameraAndPlayerNonAlloc(transformCache, hitInfos))
            {
                if (!everyMaterialHasMinAlpha)
                {
                    foreach (Material material in materials)
                    {
                        float newAlpha = material.color.a - ALPHA_CHANGE_SPEED * Time.fixedDeltaTime;
                        newAlpha = Mathf.Clamp(newAlpha, MIN_MATERIAL_ALPHA, MAX_MATERIAL_ALPHA);
                        material.color = material.color.With(null, null, null, newAlpha);
                    }

                    foreach (Material material in materials)
                    {
                        if (material.color.a > MIN_MATERIAL_ALPHA)
                        {
                            everyMaterialHasMinAlpha = false;
                        }
                    }

                    //Reset hitInfos
                    for (int i = 0; i < hitInfos.Length; i++)
                    {
                        hitInfos[i] = default;
                    }
                }
            }
            else
            {
                //Fade in
                if (!everyMaterialHasMaxAlpha)
                {
                    foreach (Material material in materials)
                    {
                        float newAlpha = material.color.a + ALPHA_CHANGE_SPEED * Time.fixedDeltaTime;
                        newAlpha = Mathf.Clamp(newAlpha, MIN_MATERIAL_ALPHA, MAX_MATERIAL_ALPHA);
                        material.color = material.color.With(null, null, null, newAlpha);
                    }

                    foreach (Material material in materials)
                    {
                        if (material.color.a < MAX_MATERIAL_ALPHA)
                        {
                            everyMaterialHasMaxAlpha = false;
                        }
                    }
                }
                else
                {
                    Destroy(this);
                }
            }
        }
        #endregion
    }
}
