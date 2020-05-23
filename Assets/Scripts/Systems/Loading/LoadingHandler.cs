using System.Collections;
using Systems.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.Loading
{
    public class LoadingHandler : MonoBehaviour
    {
#pragma warning disable CS0649
        public static LoadingHandler Instance { get; private  set; }

        [SerializeField] private GameObject debugCanvas;
        private Scene _activeLevel;
        private string _activeLevelName;

#pragma warning restore CS0649
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
            
            debugCanvas.SetActive(false);
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            debugCanvas.SetActive(true);
#endif
        }

        private void Start()
        {
            var menuLevel = SceneManager.GetSceneByName("Menu_Level");
            
            if (!menuLevel.isLoaded)
            {
                Debug.Log("Loading Menu level");
                SceneManager.LoadScene("Menu_Level", LoadSceneMode.Additive);
            }
            
            _activeLevelName = "Menu_Level";
        }

        private void OnDisable()
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public IEnumerator StartLoadSequence(int levelIndex, Transform loaderObject)
        {
            var levelName = "Level_" + levelIndex;
            
            yield return StartCoroutine(UserInterfaceHandler.Instance.HideViewOfGame());
            
            loaderObject.position = new Vector3(0,100,0);

            yield return LoadLevel(levelName);
            
            yield return StartCoroutine(UserInterfaceHandler.Instance.ShowViewOfGame());
            
            loaderObject.gameObject.SetActive(false);
        }

        private IEnumerator LoadLevel(string levelName)
        {
            if (levelName == _activeLevelName) yield return null;

            SceneManager.UnloadSceneAsync(_activeLevelName);
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
            _activeLevelName = levelName;
            yield return null;
        }
    }
}