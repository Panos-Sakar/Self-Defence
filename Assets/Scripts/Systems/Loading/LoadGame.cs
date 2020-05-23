using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systems.Loading
{
    public class LoadGame : MonoBehaviour
    {
#pragma warning disable CS0649
        // Start is called before the first frame update
        [SerializeField] private Slider progressBar;
        [SerializeField] private CanvasGroup fadeInCover;
        [SerializeField] private CanvasGroup hideLoadCanvas;
    
        [SerializeField] private float fadeDuration = 1f;
        
#pragma warning restore CS0649
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
                
            var fadeInId = LeanTween.alphaCanvas(fadeInCover, 1, fadeDuration).id;
            LeanTween.pause(fadeInId);
            var fadeOutId = LeanTween.alphaCanvas(fadeInCover, 0, fadeDuration).id;
            LeanTween.pause(fadeOutId);
            var hideMyCanvas = LeanTween.alphaCanvas(this.hideLoadCanvas, 0, 0.01f).id;
            LeanTween.pause(hideMyCanvas);
        
            StartCoroutine(StartLoadEndAnimation(fadeInId, fadeOutId, hideMyCanvas));
        }

        // Update is called once per frame
        private IEnumerator StartLoadEndAnimation(int fadeInId, int fadeOutId, int hideMyCanvas)
        {
            var gameLevel = SceneManager.LoadSceneAsync(1);
            while (!gameLevel.isDone)
            {
                progressBar.value = Mathf.Clamp01(gameLevel.progress / 0.9f);
                yield return new WaitForEndOfFrame();
            }
        
            yield return new WaitForSeconds(fadeDuration);
            LeanTween.resume(fadeInId);
            yield return new WaitForSeconds(fadeDuration + 0.1f);
            LeanTween.resume(hideMyCanvas);
            LeanTween.resume(fadeOutId);
            yield return new WaitForSeconds(fadeDuration);
        
            Destroy(this.gameObject);
        }
    }
}
