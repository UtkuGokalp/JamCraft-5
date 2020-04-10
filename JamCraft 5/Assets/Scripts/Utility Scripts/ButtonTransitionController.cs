using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

namespace Utility.Development
{
    /// <summary>
    /// Supports color tinting with multiple objects on a UI button.
    /// </summary>
    [RequireComponent(typeof(Button))]
    public class ButtonTransitionController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        #region Variables
        [Range(0, 1)]
        [SerializeField]
        private float tintValue = 0.1f;
        [Range(0f, 1f)]
        [SerializeField]
        private float mouseEnterAlpha = .5f;
        [Range(0f, 1f)]
        [SerializeField]
        private float mouseExitAlpha = 1f;
        [Range(0f, 1f)]
        [SerializeField]
        private float mouseclickAlpha = .25f;
        [Range(0f, 1f)]
        [SerializeField]
        private float buttonDisabledAlpha = 0.15f;

        private IEnumerator tintToColorCoroutine;
        private Button buttonComponent;
        private Image[] images;
        private TextMeshProUGUI[] texts;
        #endregion

        #region Awake
        private void Awake()
        {
            buttonComponent = GetComponent<Button>();
            buttonComponent.transition = Selectable.Transition.None;
            images = GetComponentsInChildren<Image>();
            texts = GetComponentsInChildren<TextMeshProUGUI>();
        }
        #endregion

        #region EnableButton
        public void EnableButton()
        {
            buttonComponent.interactable = true;
            TintToAlpha(mouseExitAlpha);
        }
        #endregion

        #region DisableButton
        public void DisableButton()
        {
            buttonComponent.interactable = false;
            TintToAlpha(buttonDisabledAlpha);
        }
        #endregion

        #region OnPointerEnter
        public void OnPointerEnter(PointerEventData eventData)
        {
            //When pointer enters the button
            if (buttonComponent.interactable)
            {
                TintToAlpha(mouseEnterAlpha);
            }
        }
        #endregion

        #region OnPointerExit
        public void OnPointerExit(PointerEventData eventData)
        {
        	//When pointer leaves the button (may be clicked the object)
            if (buttonComponent.interactable)
            {
                TintToAlpha(mouseExitAlpha);
            }
        }
        #endregion

        #region OnPointerDown
        public void OnPointerDown(PointerEventData eventData)
        {
            //When user clicks while cursor is on the button
            if (buttonComponent.interactable)
            {
                TintToAlpha(mouseclickAlpha);
            }
        }
        #endregion

        #region TintToAlpha
        private void TintToAlpha(float alphaToTint)
        {
            if (tintToColorCoroutine != null)
            {
                StopCoroutine(tintToColorCoroutine);
            }
            tintToColorCoroutine = TintToAlphaCoroutine(alphaToTint);
            StartCoroutine(tintToColorCoroutine);
        }
        #endregion

        #region TintToAlphaCoroutine
        private IEnumerator TintToAlphaCoroutine(float alphaToTint)
        {
            float currentColorAlpha = images[0].color.a;
            while (Mathf.Abs(currentColorAlpha - alphaToTint) > 0.1f)
            {
                float newAlpha = Mathf.Lerp(currentColorAlpha, alphaToTint, tintValue);
                SetAllImageColorsToNewAlpha(newAlpha);
                SetAllTextColorsToNewAlpha(newAlpha);
                yield return null;
                currentColorAlpha = newAlpha;
            }
            SetAllImageColorsToNewAlpha(alphaToTint);

            #region SetAllImageColorsToNewAlpha
            void SetAllImageColorsToNewAlpha(float newAlpha)
            {
                foreach (Image image in images)
                {
                    image.color = image.color.With(null, null, null, newAlpha);
                }
            }
            #endregion

            #region SetAllTextColorsToNewAlpha
            void SetAllTextColorsToNewAlpha(float newAlpha)
            {
                foreach (TextMeshProUGUI text in texts)
                {
                    text.color = text.color.With(null, null, null, newAlpha);
                }
            }
            #endregion
        }
        #endregion
    }
}
