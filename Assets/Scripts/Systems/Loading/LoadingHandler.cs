using System.Collections;
using SelfDef.Interfaces;
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
        [SerializeField] public int indexOffset;
        [SerializeField] public int menuLevelIndex;
        [SerializeField] private bool loadMenu;
        
        private Scene _activeLevel;
        public int activeLevelIndex;

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
            
            activeLevelIndex = menuLevelIndex;
        }

        private void OnDisable()
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public IEnumerator StartLoadSequence(int levelIndex, ICanChangeSettings loaderObject)
        {

            

            //TODO: Spawn Cube on position depending on the level
            levelIndex += indexOffset;
            if (SceneManager.sceneCountInBuildSettings == levelIndex)
            {
                levelIndex = indexOffset;
                loaderObject.LevelIndex = 1;

            }
            loaderObject.Explode(new Vector3(-10,20,-5),false);
            yield return new WaitForSeconds(1f);
            
            yield return StartCoroutine(UserInterfaceHandler.Instance.HideViewOfGame());
            
            yield return LoadLevel(levelIndex, true);
            
            loaderObject.StopAnimation = true;
            
            yield return StartCoroutine(UserInterfaceHandler.Instance.ShowViewOfGame());
            
        }

        public IEnumerator LoadLevel(int levelIndex, bool relative)
        {
            if(!relative) levelIndex += indexOffset;
            
            SceneManager.UnloadSceneAsync(activeLevelIndex);
            var asyncLoad = SceneManager.LoadSceneAsync(levelIndex, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;
            activeLevelIndex = levelIndex;
            
            while (!asyncLoad.isDone)
            {
                if (asyncLoad.progress >= 0.9f)
                {
                    asyncLoad.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}