using System.Collections;
using System.Collections.Generic;
using SelfDef.Systems.Loading;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2
{
    public class EnemySpawner : MonoBehaviour
    {
#pragma warning disable CS0649
        
        private Wave[] _waves;
        private Dictionary<EnemyTypes,GameObject> _availablePools;

        private IEnumerator _spawnCoroutine;

        private Vector3 _spawnPosition;
        
#pragma warning restore CS0649

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Initialize(SpawnPoint pointData, Dictionary<EnemyTypes,GameObject> availablePools)
        {
            name = pointData.pointName;
            _waves = pointData.waves;
            _spawnPosition = pointData.spawnPointTransform;
            gameObject.transform.position = _spawnPosition;
            
            _availablePools = availablePools;
        }

        public void OnEnable()
        {
            _spawnCoroutine = StartWaves();
            
            StartCoroutine(_spawnCoroutine);
        }

        private IEnumerator StartWaves()
        {
            foreach (var wave in _waves)
            {
                yield return new WaitForSeconds(wave.waveDelay);

                for( var i = 0; i < wave.size; i++)
                {
                    if (_availablePools.TryGetValue(wave.enemyType, out var poolRef))
                    {
                        var newEnemy = poolRef.GetComponent<PoolAttributes>().GetAvailableObject();
                        //TODO: If GetAvailableObject returned "PingPong", coroutine should weight and retry
                        if (newEnemy.name != "PingPong")
                        {
                            newEnemy.transform.position = _spawnPosition;
                            newEnemy.SetActive(true);
                        }
                    }
                    yield return new WaitForSeconds(wave.spawnRate);
                } 
            }
            yield return new WaitForSeconds(1f);
            LoadingHandler.Instance.playerFinishedLevel.Invoke();
        }
    }
}
