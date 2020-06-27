using System;
using System.Collections;
using JetBrains.Annotations;
using SelfDef.Interfaces;
using SelfDef.Systems.Loading;
using SelfDef.Variables;
using Unity.Mathematics;
using UnityEngine;

namespace SelfDef.Change_Cubes
{
    public class AudioLevels : MonoBehaviour, ICanChangeSettings
    {
#pragma warning disable CS0649
        
        public Vector3 StartPosition { get; set; }
        public bool StopAnimation { get; set; }
        
        [Header("Audio Sources")]
        [SerializeField] 
        private AudioSource[] musicSources;
        [SerializeField] 
        private AudioSource[] sfxSources;

        [Header("References")]
        [SerializeField]
        private GameObject archesInner;
        [SerializeField]
        private GameObject archesOuter;

        [SerializeField] 
        private Animator animator;
        
        [Header("Levels")]
        [SerializeField]
        private AudioVariables audioLevels;

        private bool _animationLock;
        private LoadingHandler _loadingHandler;
        private static readonly int Hide = Animator.StringToHash("Hide");

#pragma warning restore CS0649
        private void Awake()
        {
            _loadingHandler = LoadingHandler.Instance;
            
            StartPosition = new Vector3(-3,40,-13);
            
            StopAnimation = false;
            _animationLock = false;

            switch (audioLevels.muteLevel)
            {
                case AudioVariables.MuteLevel.Mute:
                    ChangeLevels(true, true);
                    break;
                case AudioVariables.MuteLevel.OnlySfx:
                    ChangeLevels(true, false);
                    break;
                case AudioVariables.MuteLevel.FullAudio:
                    ChangeLevels(false, false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            _loadingHandler.playerFinishedLevel.AddListener(ResetPosition);
            _loadingHandler.levelLoadingStarted.AddListener(Lock);
            
        }
        private void Update()
        {
            if (StopAnimation)
            {
                StopAnimation = false;
                _animationLock = false;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            var colliderSettings = other.GetComponent<IActivateSettings>();

            if (colliderSettings == null) return;

            if (!colliderSettings.ChangeLevel) return;
            
            if(_animationLock) return;
            
            StartCoroutine(Explode(0f));
        }

        public IEnumerator Explode(float delay)
        {
            _animationLock = true;
            switch (audioLevels.muteLevel)
            {
                case AudioVariables.MuteLevel.Mute:
                    audioLevels.muteLevel = AudioVariables.MuteLevel.FullAudio;
                    
                    ChangeLevels(false, false);
                    
                    break;
                case AudioVariables.MuteLevel.OnlySfx:
                    audioLevels.muteLevel = AudioVariables.MuteLevel.Mute;
                    
                    ChangeLevels(true, true);
                    
                    break;
                case AudioVariables.MuteLevel.FullAudio:
                    audioLevels.muteLevel = AudioVariables.MuteLevel.OnlySfx;
                    
                    ChangeLevels(true, false);
                    
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            yield return new WaitForSeconds(delay);
            StopAnimation = true;
        }

        private void ChangeLevels(bool muteMusic, bool muteSfx)
        {
            Music(muteMusic);
            Sfx(muteSfx);

            archesOuter.SetActive(!muteMusic);
            archesInner.SetActive(!muteSfx);
        }

        private void Music(bool mute)
        {
            foreach (var musicSource in musicSources)
            {
                musicSource.mute = mute;
            }
        }

        private void Sfx(bool mute)
        {
            foreach (var sfxSource in sfxSources)
            {
                sfxSource.mute = mute;
            }
        }

        public void ResetPosition()
        {
            var obj = gameObject;
            
            obj.transform.position = StartPosition;
            obj.transform.localScale = Vector3.one;
            obj.transform.localRotation = quaternion.identity;
            animator.
            gameObject.SetActive(true);
        }

        public void Lock()
        {
            animator.SetTrigger(Hide);
        }

        [UsedImplicitly]
        private void Disappear()
        {
            gameObject.SetActive(false);
        }

        public void Kill()
        {
            _loadingHandler.playerFinishedLevel.RemoveListener(ResetPosition);
            _loadingHandler.levelLoadingStarted.RemoveListener(Lock);
            Destroy(this.gameObject);
        }
    }
}
