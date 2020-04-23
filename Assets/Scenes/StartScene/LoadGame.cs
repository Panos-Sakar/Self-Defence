using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider progressBar;
    [SerializeField] private CanvasGroup fadeCover;
    [SerializeField] private CanvasGroup hideCanvas;
    [SerializeField] private float fadeDuration = 1f;
    
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        int fadeInId = LeanTween.alphaCanvas(fadeCover, 1, fadeDuration).id;
        LeanTween.pause(fadeInId);
        int fadeOutId = LeanTween.alphaCanvas(fadeCover, 0, fadeDuration).id;
        LeanTween.pause(fadeOutId);
        int hideMyCanvas = LeanTween.alphaCanvas(this.hideCanvas, 0, 0.01f).id;
        LeanTween.pause(hideMyCanvas);
        
        StartCoroutine(StartLoadEndAnimation(fadeInId, fadeOutId, hideMyCanvas));
    }

    // Update is called once per frame
    IEnumerator StartLoadEndAnimation(int fadeInId, int fadeOutId, int hideMyCanvas)
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(1);
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
