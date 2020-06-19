using System;
using UnityEngine;
// ReSharper disable NotAccessedField.Global


namespace SelfDef.Systems.SpawnSystemV2
{
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
        public GameObject enemyPrefab;
        public int size;
        public bool canGrow;
    }

    [Serializable]
    public class SpawnPoint
    {
        public string pointName;
        public Vector3 spawnPointTransform;
        public int[] enemies;
    }
}
