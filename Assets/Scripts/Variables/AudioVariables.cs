using System;
using UnityEngine;

namespace SelfDef.Variables
{
    [Serializable]
    [CreateAssetMenu(menuName = "Public Variables/AudioVariable",fileName = "AudioVariable")]
    public class AudioVariables : ScriptableObject
    {
        [Header("Levels")] 
        public int audioLevel;

        public MuteLevel muteLevel;
        
        public enum MuteLevel
        {
            Mute = 0,
            OnlySfx = 1,
            FullAudio = 2
        }
    }
}
