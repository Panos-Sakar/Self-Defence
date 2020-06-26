using SelfDef.Systems.Loading;
using System.Collections.Generic;
using SelfDef.Variables;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2
{
    public class Initializer : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [Header("Prefabs")] 
        private GameObject _playerRef;
        [SerializeField] 
        private PersistentVariables persistentVariable;
        
        [Header("Prefabs")] 
        [SerializeField] 
        private GameObject poolPrefab;
        [SerializeField]
        private GameObject spawnPointPrefab;
        
        [Header("Positions")] 
        [SerializeField] 
        private Transform poolsTransform;
        [SerializeField] 
        private Transform spawnPointsTransform;
        
        [Header("Data")]
        [SerializeField]
        private LevelSpawnData levelSpawnData;
        
        private readonly Dictionary<EnemyTypes,GameObject> _availablePools = new Dictionary<EnemyTypes, GameObject>();
        private readonly Dictionary<string,GameObject> _spawnPoints = new Dictionary<string, GameObject>();

#pragma warning restore CS0649
        
        private void Awake()
        {
            _playerRef = LoadingHandler.Instance.playerRef;
        }

        private void OnEnable()
        {
            persistentVariable.enemySpawnFinished = levelSpawnData.spawnPoints.Length;
            
            foreach (var pool in levelSpawnData.availablePools)
            {
                var poolInstance = Instantiate(poolPrefab, poolsTransform);
                poolInstance.GetComponent<PoolAttributes>().InitializePool(pool,_playerRef);
                
                _availablePools.Add(pool.enemyType,poolInstance);
            }

            foreach (var point in levelSpawnData.spawnPoints)
            {
                var poolInstance = Instantiate(spawnPointPrefab, spawnPointsTransform);
                poolInstance.GetComponent<EnemySpawner>().Initialize(point,_availablePools);
                
                _spawnPoints.Add(point.pointName,poolInstance);
            }

            foreach (var point in _spawnPoints)
            {
                point.Value.SetActive(true);
            }
        }

        public LevelSpawnData GetLevelData() => levelSpawnData;
    }
}
