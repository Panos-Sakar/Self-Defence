using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingHandler : MonoBehaviour
{
    public static LoadingHandler Instance { get; private  set; }
    
    [SerializeField] private GameObject[] debugObjects;
    private Scene _activeLevel;
    private string _activeLevelName;
    private bool _firstLoad;
    
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
        #if UNITY_EDITOR
        ActivateDebug();
        #endif
    }
    
    void Start()
    {
        LoadLevel("Level_1");
    }
    
    void OnDisable()
    {

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    [ContextMenu("ActivateDebug")]
    public void ActivateDebug()
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