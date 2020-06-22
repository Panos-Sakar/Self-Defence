using System.Collections.Generic;
using System.Linq;
using SelfDef.NPCScripts;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2
{
    public class PoolAttributes : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [HideInInspector]
        public GameObject playerRef;
        
        [Header("References")]
        [SerializeField] 
        private GameObject pingPong;

        [Header("Attributes")]
        public EnemyTypes enemyType;
        
        private Transform _myTransform;
        private EnemyPool _poolData;
        private int _poolSize;

        private readonly List<GameObject> _pools = new List<GameObject>();

#pragma warning restore CS0649
        
        public void InitializePool(EnemyPool poolData, GameObject inPlayerRef)
        {
            _myTransform = gameObject.transform;
            playerRef = inPlayerRef;
            _poolData = poolData;

            name = _poolData.poolName;
            enemyType = _poolData.enemyType;

            for (var i = 0; i < _poolData.size; i++)
            {
                _poolSize++;
                
                var pooledObject = Instantiate(_poolData.enemyPrefab, _myTransform);
                pooledObject.GetComponent<EnemyLogic>().Initialize($"{_poolData.enemyType}_{_poolSize}", playerRef);
                pooledObject.SetActive(false);
                
                _pools.Add(pooledObject);
            }
        }
        
        public GameObject GetAvailableObject()
        {
            foreach (var pool in _pools.Where(pool => pool.activeInHierarchy == false))
            {
                return pool;
            }

            if (_poolData.canGrow)
            {
                _poolSize++;
                
                var pooledObject = Instantiate(_poolData.enemyPrefab, _myTransform);
                pooledObject.SetActive(false);
                pooledObject.GetComponent<EnemyLogic>().Initialize($"{_poolData.enemyType}_{_poolSize}", playerRef);
                
                _pools.Add(pooledObject);
                return pooledObject;
            }
            else
            {
                return pingPong;
            }
        }
    }
}
