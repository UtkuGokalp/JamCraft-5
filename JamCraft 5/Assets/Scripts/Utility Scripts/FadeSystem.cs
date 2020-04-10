using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Utility.Development
{
    public class FadeSystem : MonoBehaviour
    {
        #region Variables
        [SerializeField]
        private bool dontDestroyOnLoad;
        [SerializeField]
        private Vector2Int referenceResolution = new Vector2Int(1920, 1080);
        [SerializeField]
        private Color fadeImageColor = Color.black;
        private Canvas canvas;
        private Image fadeImage;
        private bool Fading => fadeImage.gameObject.activeSelf;
        /// <summary>
        /// Current instance of the FadeSystem. If don't destroy on load is false, returns null.
        /// </summary>
        public static FadeSystem Instance { get; private set; }
        #endregion

        #region Awake
        private void Awake()
        {
            if (Instance == null)
            {
                canvas = CreateCanvas();
                fadeImage = CreateFadeImage();

                if (dontDestroyOnLoad)
                {
                    Instance = this;
                    DontDestroyOnLoad(gameObject);
                    DontDestroyOnLoad(canvas.gameObject);
                }
                else
                {
                    Instance = null;
                }
            }
            else
            {
                Destroy(gameObject);
            }

            #region CreateCanvas
            Canvas CreateCanvas()
            {
                GameObject canvasGameObject = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
                Canvas canvasComponent = canvasGameObject.GetComponent<Canvas>();
                CanvasScaler canvasScalerComponent = canvasGameObject.GetComponent<CanvasScaler>();

                canvasComponent.sortingOrder = 32767;
                canvasComponent.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasScalerComponent.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
                canvasScalerComponent.referenceResolution = referenceResolution;

                return canvasComponent;
            }
            #endregion

            #region CreateFadeImage
            Image CreateFadeImage()
            {
                GameObject fadeImageObject = new GameObject("Fade Image", typeof(Image));
                fadeImageObject.transform.SetParent(canvas.transform);
                RectTransform fadeImageRectTransform = fadeImageObject.GetComponent<RectTransform>();
                fadeImageRectTransform.anchorMin = Vector2.zero;
                fadeImageRectTransform.anchorMax = Vector2.one;
                fadeImageRectTransform.pivot = Vector2.one / 2;
                fadeImageRectTransform.anchoredPosition = Vector2.zero;
                fadeImageObject.SetActive(false);
                Image fadeObjectImageComponent = fadeImageObject.GetComponent<Image>();
                fadeObjectImageComponent.color = fadeImageColor.With(null, null, null, 0);

                return fadeObjectImageComponent;
            }
            #endregion
        }
        #endregion

        #region Fade
        /// <summary>
        /// Makes the game fade in given amount of seconds and then executes the given action.
        /// </summary>
        public void Fade(float fadeSeconds, System.Action actionWhenFaded)
        {
            if (!Fading)
            {
                fadeImage.gameObject.SetActive(true);
                StartCoroutine(FadeCoroutine(fadeSeconds, actionWhenFaded));
            }
        }
        #endregion

        #region FadeCoroutine
        private IEnumerator FadeCoroutine(float fadeSeconds, System.Action actionWhenFaded)
        {
            //Multiplied by two because fadeSeconds should determine when the fade ends.
            //If not multiplied, fade in and fade out ends separately in fadeSeconds.
            //(Ex: 2 seconds for fade in and another 2 for fade out)
            float fadeSpeed = (1 / fadeSeconds) * 2;

            while (fadeImage.color.a < 1)
            {
                fadeImage.color = fadeImage.color.With(null, null, null, fadeImage.color.a + Time.deltaTime * fadeSpeed);
                yield return null;
            }
            fadeImage.color = fadeImage.color.With(null, null, null, 1);

            actionWhenFaded?.Invoke();

            while (fadeImage.color.a > 0)
            {
                fadeImage.color = fadeImage.color.With(null, null, null, fadeImage.color.a - Time.deltaTime * fadeSpeed);
                yield return null;
            }
            fadeImage.color = fadeImage.color.With(null, null, null, 0);
            fadeImage.gameObject.SetActive(false);
        }
        #endregion
    }
}
