using System.Collections;
using SelfDef.Systems.UI;
using SelfDef.Variables;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SelfDef.Systems.Loading
{
    public class LoadingHandler : MonoBehaviour
    {
#pragma warning disable CS0649
        public static LoadingHandler Instance { get; private  set; }
        [HideInInspector]
        public UnityEvent playerFinishedLevel;
        public UnityEvent levelLoadingStarted;
        [Header("References")]
        [SerializeField] 
        public GameObject playerRef;
        [SerializeField] 
        private GameObject debugCanvas;
        [SerializeField]
        private PersistentVariables persistentVariable;
        
        [Header("Level Indexing")]
        [SerializeField] public int indexOffset;
        [SerializeField] public int menuLevelIndex;
        [SerializeField] public bool loadMenu;
        
        private Scene _activeLevel;
        [HideInInspector]
        public int activeLevelIndex;

#pragma warning restore CS0649
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }
            
            debugCanvas.SetActive(false);
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
            
            activeLevelIndex = menuLevelIndex;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // ReSharper disable once InvertIf
            if (mode == LoadSceneMode.Additive)
            {
                persistentVariable.currentLevelIndex = activeLevelIndex;
                persistentVariable.activeEnemies = 0;
                persistentVariable.loading = false;
            }
            
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            debugCanvas.SetActive(true);
#endif
        }

        private void Update()
        {
            if (persistentVariable.enemySpawnFinished ==0 
                && persistentVariable.activeEnemies == 0 
                && !persistentVariable.loading)
            {
                persistentVariable.loading = true;
                playerFinishedLevel.Invoke();
            }
        }

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDisable()
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void StartLoadSequence(int levelIndex, bool relative)
        {
            StartCoroutine(LoadLevel(levelIndex, relative));
        }

        private IEnumerator LoadLevel(int levelIndex, bool relative)
        {
            levelLoadingStarted.Invoke();
            
            if(relative) levelIndex += indexOffset;

            yield return StartCoroutine(UserInterfaceHandler.Instance.HideViewOfGame());
            
            if(activeLevelIndex >= 0) SceneManager.UnloadSceneAsync(activeLevelIndex);
            var asyncLoad = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
            activeLevelIndex = levelIndex;
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            
            yield return StartCoroutine(UserInterfaceHandler.Instance.ShowViewOfGame());
        }
    }
}