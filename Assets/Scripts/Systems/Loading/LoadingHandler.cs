using Systems.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Systems.Loading
{
    public class LoadingHandler : MonoBehaviour
    {
#pragma warning disable CS0649
        public static LoadingHandler Instance { get; private  set; }
        
        [SerializeField] private GameObject[] debugObjects;
        private Scene _activeLevel;
        private string _activeLevelName;
        private bool _firstLoad;
    
#pragma warning restore CS0649
        void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

            _firstLoad = true;
        }
    
        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
#if DEVELOPMENT_BUILD || UNITY_EDITOR
            ActivateDebug();
#endif
        }
    
        void Start()
        {
            LoadLevel("Menu_Level");
            
#if UNITY_EDITOR
            UserInterfaceHandler.Instance.PrintToDebug(4,"");
#endif
        } 
    
        void OnDisable()
        {

            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        private void ActivateDebug()
        {
            foreach (GameObject obj in debugObjects)
            {
                obj.SetActive(true);
            }
        }

        public void LoadLevel(string levelName)
        {
            if (levelName != _activeLevelName)
            {
                if (_firstLoad)
                {
                    _firstLoad = false;
                }
                else
                {
                    SceneManager.UnloadSceneAsync(_activeLevelName);
                }
            
                SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
                _activeLevelName = levelName;

            }
        }
    }
}