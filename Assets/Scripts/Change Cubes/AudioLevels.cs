using System;
using SelfDef.Systems.Loading;
using SelfDef.Variables;
using UnityEngine;

namespace SelfDef.Change_Cubes
{
    public class AudioLevels : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [Header("Audio Sources")]
        [SerializeField] 
        private AudioSource[] musicSources;
        [SerializeField] 
        private AudioSource[] sfxSources;

        [Header("Arches References")]
        [SerializeField]
        private GameObject archesInner;
        [SerializeField]
        private GameObject archesOuter;
        
        [Header("Levels")]
        [SerializeField]
        private AudioVariables audioLevels;

        private Vector3 _startPosition;
        
        private LoadingHandler _loadingHandler;
        
#pragma warning restore CS0649
        private void Awake()
        {
            _startPosition = new Vector3(-3,-2,-13);
            _loadingHandler = LoadingHandler.Instance;
            _loadingHandler.playerFinishedLevel.AddListener(ResetPosition);
            
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
        }

        private void OnTriggerEnter(Collider other)
        {
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
            Debug.Log("Reset");
            //gameObject.transform.position = _startPosition;
            gameObject.SetActive(true);
        }

        public void Kill()
        {
            _loadingHandler.playerFinishedLevel.RemoveListener(ResetPosition);
            Destroy(this.gameObject);
        }
    }
}
