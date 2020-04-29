using System.Collections.Generic;
using UnityEngine;

namespace Systems.SpawnSystem
{
    public class EnemyPool : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField] private GameObject pinPong;

        private GameObject _enemyPrefab;
        private int _poolDepth;
        private bool _canGrow;
        private string _enemyName;

        private readonly List<GameObject> _pool = new List<GameObject>();
        
#pragma warning restore CS0649

        public GameObject GetAvailableObject()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].activeInHierarchy == false)
                {
                    return _pool[i];
                }
            }

            if (_canGrow)
            {
                GameObject pooledObject = Instantiate(_enemyPrefab);
                pooledObject.SetActive(false);
                _pool.Add(pooledObject);
                return pooledObject;
            }
            else
            {
                return pinPong;
            }
        }

        public void InitializePool(GameObject enemyPrefab,string enemyName, int poolDepth, bool canGrow)
        {
            _enemyPrefab = enemyPrefab;
            _enemyName = enemyName;
            _poolDepth = poolDepth;
            _canGrow = canGrow;
            
            for (int i = 0; i < _poolDepth; i++)
            {
                GameObject pooledObject = Instantiate(_enemyPrefab,gameObject.transform);
                pooledObject.SetActive(false);
                _pool.Add(pooledObject);
            }
        }
    }
}
