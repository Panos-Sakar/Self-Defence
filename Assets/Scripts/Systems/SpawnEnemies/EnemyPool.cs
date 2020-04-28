using System.Collections.Generic;
using UnityEngine;

namespace Systems.SpawnEnemies
{
    public class EnemyPool : MonoBehaviour
    {
#pragma warning disable CS0649
        // Start is called before the first frame update
        [SerializeField] private GameObject enemyPrefab;
        [SerializeField] private GameObject pinPong;
    
        [SerializeField] private int poolDepth;
        [SerializeField] private bool canGrow = true;

        private readonly List<GameObject> _pool = new List<GameObject>();
        
#pragma warning restore CS0649
        private void Awake()
        {
            for (int i = 0; i < poolDepth; i++)
            {
                GameObject pooledObject = Instantiate(enemyPrefab,gameObject.transform);
                pooledObject.SetActive(false);
                _pool.Add(pooledObject);
            }
        }

        public GameObject GetAvailableObject()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i].activeInHierarchy == false)
                {
                    return _pool[i];
                }
            }

            if (canGrow)
            {
                GameObject pooledObject = Instantiate(enemyPrefab);
                pooledObject.SetActive(false);
                _pool.Add(pooledObject);
                return pooledObject;
            }
            else
            {
                return pinPong;
            }

        }
    }
}
