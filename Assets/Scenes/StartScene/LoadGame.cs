using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGame : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Slider progressBar;
    void Start()
    {
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    IEnumerator LoadAsyncOperation()
    {
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(1);
        while (!gameLevel.isDone)
        {
            progressBar.value = Mathf.Clamp01(gameLevel.progress / 0.9f);
            yield return new WaitForEndOfFrame();
        }
    }
}
