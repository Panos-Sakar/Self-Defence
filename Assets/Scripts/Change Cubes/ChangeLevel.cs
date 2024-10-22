﻿using System.Collections;
using SelfDef.Interfaces;
using SelfDef.PlayerScripts;
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

        [SerializeField]
        private string tipText;
        [SerializeField] 
        private Sprite icon;
        public string TipText
        {
            get => tipText;
            set => tipText = value;
        }

        public Vector3 StartPosition { get; set; }

        [SerializeField]
        private int levelIndex;
        [SerializeField]
        private MeshRenderer[] meshRenderers;
        [SerializeField]
        private GameObject animationChild;

        [SerializeField] private int staminaGift;
        

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

        public (bool, string) GetSecondaryText()
        {
            return (false, "");
        }

        public IEnumerator Explode(float delay)
        {
            _animationLock = true;
            
            _loadingHandler.levelLoadingStarted.Invoke();
            
            yield return new WaitForEndOfFrame();
            
            StartCoroutine(ExplodeEffect(delay,new Vector3(-10,20,-5),false));
            StartCoroutine(_userInterfaceHandler.HideViewOfGame());
            
            
            yield return new WaitForSeconds(delay);
            
            yield return StartCoroutine(LoadLevel(CorrectIndex(LevelIndex), false));
            
            yield return StartCoroutine(_userInterfaceHandler.ShowViewOfGame());
            
            LevelIndex++;
            StopAnimation = true;
        }

        private  IEnumerator ExplodeEffect(float delay, Vector3 newPosition, bool destroyAfterExplode)
        {
            StartPosition = newPosition;
            DisableMeshRenderers(false);
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
            
            DisableMeshRenderers(true);
            
            animationChild.SetActive(false);
        }

        public void Kill()
        {
            _loadingHandler.playerFinishedLevel.RemoveListener(ResetPosition);
            Destroy(_mainObject.gameObject);
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        private int CorrectIndex(int index)
        {
            var correctIndex = index + _loadingHandler.indexOffset;
            
            if (SceneManager.sceneCountInBuildSettings == correctIndex)
            {
                correctIndex = _loadingHandler.indexOffset;
                LevelIndex = 0;
            }

            return correctIndex;
        }

        private IEnumerator LoadLevel(int index, bool relative)
        {
            if(relative) index += _loadingHandler.indexOffset;
            
            if(_loadingHandler.activeLevelIndex >= 0) SceneManager.UnloadSceneAsync(_loadingHandler.activeLevelIndex);
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
            PlayerLogicScript.Instance.GiveStamina(staminaGift);
            staminaGift++;
            if (staminaGift > 5) staminaGift = 1;
        }

        public void ResetPosition(Vector3 newPosition)
        {
            StartPosition = newPosition;
            gameObject.transform.localPosition = new Vector3(0,0,0);
            _mainObject.position = StartPosition;
            _mainObject.gameObject.SetActive(true);
        }

        private void DisableMeshRenderers(bool disable)
        {
            foreach (var meshRenderer in meshRenderers)
            {
                meshRenderer.enabled = disable;
            }
        }
    }
}
