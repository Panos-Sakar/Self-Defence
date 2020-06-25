using System.Collections;
using SelfDef.Interfaces;
using SelfDef.Systems.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SelfDef.Systems.Loading
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

        private bool _stopAnimation;
        private bool _animationLock;

#pragma warning restore CS0649
        private void Awake()
        {
            _stopAnimation = false;
            _animationLock = false;
            //animationChild.GetComponent<Animator>().
            _mainObject = gameObject.transform.parent;
            LevelIndex = levelIndex;
            StartPosition = _mainObject.position;
            LoadingHandler.Instance.playerFinishedLevel.AddListener(ResetPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            //DontDestroyOnLoad(_mainObject);
            
            var colliderSettings = other.GetComponent<IActivateSettings>();

            if (colliderSettings == null) return;

            if (!colliderSettings.ChangeLevel) return;
            
            if(_animationLock) return;
            
            //StartCoroutine(LoadingHandler.Instance.StartLoadSequence(LevelIndex, this));
            
            StartCoroutine(TestRun());
            
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

        public IEnumerator Explode(Vector3 newPosition , bool destroyAfterExplode)
        {
            StartPosition = newPosition;
            yield return StartCoroutine(ExplodeEffect(destroyAfterExplode));
        }

        private void Update()
        {
            if (_stopAnimation)
            {
                _stopAnimation = false;
                _animationLock = false;
                _mainObject.gameObject.SetActive(false);
            }
        }

        public void Kill()
        {
            LoadingHandler.Instance.playerFinishedLevel.RemoveListener(ResetPosition);
            Destroy(_mainObject.gameObject);
        }

        public void Lock()
        {
            var obj = gameObject;
            obj.transform.localPosition = new Vector3(0,0,0);
            obj.transform.localRotation = new Quaternion().normalized;
            _mainObject.position = new Vector3(-10,100,-10);
            meshRenderer.enabled = true;
            
            animationChild.SetActive(false);

            //_mainObject.gameObject.SetActive(false);
        }

        private  IEnumerator ExplodeEffect(bool destroyAfterExplode)
        {

            meshRenderer.enabled = false;
            animationChild.SetActive(true);
            
            yield return new WaitForSeconds(1f);

            Lock();
            if(destroyAfterExplode) Kill();
        }

        private IEnumerator TestRun()
        {
            _animationLock = true;
            yield return StartCoroutine(Explode(new Vector3(-10,20,-5),false));
            yield return StartCoroutine(UserInterfaceHandler.Instance.HideViewOfGame());
            yield return StartCoroutine(LoadLevel(CorrectIndex(LevelIndex), true));
            yield return StartCoroutine(UserInterfaceHandler.Instance.ShowViewOfGame());
            LevelIndex++;
            _stopAnimation = true;
            
        }

        private int CorrectIndex(int index)
        {
            var correctIndex = index + LoadingHandler.Instance.indexOffset;
            
            if (SceneManager.sceneCountInBuildSettings == correctIndex)
            {
                correctIndex = LoadingHandler.Instance.indexOffset;
                LevelIndex = 1;
            }

            return correctIndex;
        }

        private static IEnumerator LoadLevel(int index, bool relative)
        {
            if(!relative) index += LoadingHandler.Instance.indexOffset;
            
            SceneManager.UnloadSceneAsync(LoadingHandler.Instance.activeLevelIndex);
            var asyncLoad = SceneManager.LoadSceneAsync(index, LoadSceneMode.Additive);
            asyncLoad.allowSceneActivation = false;
            LoadingHandler.Instance.activeLevelIndex = index;
            
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
