using System;
using UnityEngine;
// ReSharper disable NotAccessedField.Global

namespace SelfDef.Systems.SpawnSystemV2
{

    public enum EnemyTypes
    {
        SmallBall = 0,
        BigBall = 1
    }
    
    [Serializable]
    [CreateAssetMenu]
    public class SpawnDataOfLevel : ScriptableObject
    {
        public SpawnPoint[] spawnPoints;
        public EnemyPool[] availablePools;
    }

    [Serializable]
    public class EnemyPool
    {
        public string poolName;
        public EnemyTypes enemyType;
        public GameObject enemyPrefab;
        public int size;
        public bool canGrow;
    }

    [Serializable]
    public class SpawnPoint
    {
        public string pointName;
        public Vector3 spawnPointTransform;
        public Wave[] waves;
    }
    
    [Serializable]
    public class Wave
    {
        public string waveName;
        public float waveDelay;
        public int size;
        public EnemyTypes enemyType;
        public float spawnRate;
        
    }
}
