using System.Collections;
using SelfDef.Interfaces;
using SelfDef.Systems.Loading;
using SelfDef.Systems.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelfDef.Change_Cubes
{
    public class ChangeLevel : MonoBehaviour, ICanChangeSettings
    {
#pragma warning disable CS0649

        public int LevelIndex { get; set; }
        public bool StopAnimation { get; set; }

        public Vector3 StartPosition { get; set; }

        [SerializeField]
        private int levelIndex;
        [SerializeField]
        private MeshRenderer meshRenderer;
        [SerializeField]
        private GameObject animationChild;
        

        private Transform _mainObject;
        
        private bool _animationLock;

        private LoadingHandler _loadingHandler;
        private UserInterfaceHandler _userInterfaceHandler;

#pragma warning restore CS0649
        private void Awake()
        {
            _mainObject = gameObject.transform.parent;
            
            LevelIndex = levelIndex;
            StartPosition = _mainObject.position;

            StopAnimation = false;
            _animationLock = false;

            _loadingHandler = LoadingHandler.Instance;
            _userInterfaceHandler = UserInterfaceHandler.Instance;
            _loadingHandler.playerFinishedLevel.AddListener(ResetPosition);
        }

        private void Update()
        {
            if (StopAnimation)
            {
                StopAnimation = false;
                _animationLock = false;
                _mainObject.gameObject.SetActive(false);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var colliderSettings = other.GetComponent<IActivateSettings>();

            if (colliderSettings == null) return;

            if (!colliderSettings.ChangeLevel) return;
            
            if(_animationLock) return;
            
            StartCoroutine(Explode(1f));
            
        }

        public IEnumerator Explode(float delay)
        {
            _animationLock = true;
            
            StartCoroutine(ExplodeEffect(delay,new Vector3(-10,20,-5),false));
            StartCoroutine(_userInterfaceHandler.HideViewOfGame());
            _loadingHandler.levelLoadingStarted.Invoke();
            
            yield return new WaitForSeconds(delay);
            
            yield return StartCoroutine(LoadLevel(CorrectIndex(LevelIndex), false));
            
            yield return StartCoroutine(_userInterfaceHandler.ShowViewOfGame());
            
            LevelIndex++;
            StopAnimation = true;
        }

        private  IEnumerator ExplodeEffect(float delay, Vector3 newPosition, bool destroyAfterExplode)
        {
            StartPosition = newPosition;
            meshRenderer.enabled = false;
            animationChild.SetActive(true);
            
            yield return new WaitForSeconds(delay);

            Lock();
            
            if(destroyAfterExplode) Kill();
        }

        public void Lock()
        {
            var obj = gameObject;
            obj.transform.localPosition = new Vector3(0,0,0);
            obj.transform.localRotation = new Quaternion().normalized;
            
            _mainObject.position = new Vector3(-10,50,-10);
            
            meshRenderer.enabled = true;
            
            animationChild.SetActive(false);
        }

        public void Kill()
        {
            _loadingHandler.playerFinishedLevel.RemoveListener(ResetPosition);
            Destroy(_mainObject.gameObject);
        }

        private int CorrectIndex(int index)
        {
            var correctIndex = index + _loadingHandler.indexOffset;
            
            if (SceneManager.sceneCountInBuildSettings == correctIndex)
            {
                correctIndex = _loadingHandler.indexOffset;
                LevelIndex = 1;
            }

            return correctIndex;
        }

        private IEnumerator LoadLevel(int index, bool relative)
        {
            if(relative) index += _loadingHandler.indexOffset;
            
            SceneManager.UnloadSceneAsync(_loadingHandler.activeLevelIndex);
            var asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            _loadingHandler.activeLevelIndex = index;
            
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
        }

        public void ResetPosition()
        {
            gameObject.transform.localPosition = new Vector3(0,0,0);
            _mainObject.position = StartPosition;
            _mainObject.gameObject.SetActive(true);
        }

        public void ResetPosition(Vector3 newPosition)
        {
            
            StartPosition = newPosition;
            gameObject.transform.localPosition = new Vector3(0,0,0);
            _mainObject.position = StartPosition;
            _mainObject.gameObject.SetActive(true);
            
        }
    }
}
