using System.Collections;
using SelfDef.Systems.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SelfDef.Systems.Loading
{
    public class LoadingHandler : MonoBehaviour
    {
#pragma warning disable CS0649
        public static LoadingHandler Instance { get; private  set; }
        public UnityEvent playerFinishedLevel;
        
        [SerializeField] public GameObject playerRef;
        
        [SerializeField] private GameObject debugCanvas;
        [SerializeField] private int menuLevelIndex;
        [SerializeField] private bool loadMenu;
        
        private Scene _activeLevel;
        private int _activeLevelIndex;

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
#if !UNITY_EDITOR
            loadMenu = true;
#endif
            if (!loadMenu) return;
            
            var menuLevel = SceneManager.GetSceneByName("Menu_Level");
            
            if (!menuLevel.isLoaded)
            {
                SceneManager.LoadScene(menuLevelIndex, LoadSceneMode.Additive);
            }
            
            _activeLevelIndex = menuLevelIndex;
        }

        private void OnDisable()
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public IEnumerator StartLoadSequence(int levelIndex, Transform loaderObject)
        {

            yield return StartCoroutine(UserInterfaceHandler.Instance.HideViewOfGame());
            
            loaderObject.position = new Vector3(0,100,0);

            yield return LoadLevel(levelIndex);

            yield return StartCoroutine(UserInterfaceHandler.Instance.ShowViewOfGame());
            
            loaderObject.gameObject.SetActive(false);
        }

        private IEnumerator LoadLevel(int levelIndex)
        {
            levelIndex += menuLevelIndex;
            SceneManager.UnloadSceneAsync(_activeLevelIndex);
            SceneManager.LoadScene(levelIndex, LoadSceneMode.Additive);
            _activeLevelIndex = levelIndex;
            yield return null;
        }
    }
}