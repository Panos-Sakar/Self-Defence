using System;
using System.Collections.Generic;
using Scenes.Levels;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systems.SpawnSystem
{
    public class SpawnPoint : MonoBehaviour
    {
#pragma warning disable CS0649

        private Transform _myTransform;
        private string _pointName;
        private float _spawnRate;
        private string[] _pattern;
        private Dictionary<string,GameObject> _pools;
        private float _randomize;

        private int _patternPosition;
        
#pragma warning restore CS0649
        private void Awake()
        {
            _myTransform = transform;
            _patternPosition = 0;
            _randomize = Random.Range(0.1f, 0.3f);
        }

        public  void InitializePoint(Dictionary<string,GameObject> pools, string pointName, float spawnRate, string[] pattern)
        {
            _pointName = pointName;
            _spawnRate = spawnRate;
            _pattern = pattern;
            _pools = pools;

            InvokeRepeating(nameof(SpawnEnemy), _spawnRate + _randomize, _spawnRate + _randomize);
        }
        
        void SpawnEnemy()
        {
            GameObject enemy = _pools[_pattern[_patternPosition]].GetComponent<EnemyPool>().GetAvailableObject();
        
            enemy.transform.position = _myTransform.position;
            enemy.SetActive(true);
            
            _patternPosition++;

            if (_patternPosition >= _pattern.Length)
            {
                CancelInvoke(nameof(SpawnEnemy));
            }
        }
    }
}
