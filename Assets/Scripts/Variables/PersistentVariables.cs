using System;
using UnityEngine;

namespace SelfDef.Variables
{
    [Serializable]
    [CreateAssetMenu(menuName = "Public Variables/PersistentVariables",fileName = "PersistentVariables")]
    public class PersistentVariables : ScriptableObject
    {
        [Header("Indexing")]
        public int currentLevelIndex;
        
        [Header("Enemy Spawning")]
        public int activeEnemies;
        public bool enemySpawnFinished;

        [Header("Misc")] 
        public bool loading;
    }
}
