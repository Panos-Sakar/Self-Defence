﻿using System.Collections.Generic;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystem
{
    public class SpawnInit : MonoBehaviour
    {
#pragma warning disable CS0649
        
        private string _pathToJson ;
        private SpawnData _spawnData;

        [SerializeField] private string levelIndex = "Test";
        [Space]
        [SerializeField] private GameObject poolPrefab;
        [SerializeField] private GameObject spawnPointPrefab;
        [Space]
        [SerializeField] private GameObject[] enemyPrefabs;

        private readonly Dictionary<string,GameObject> _availablePools = new Dictionary<string, GameObject>();
        private readonly Dictionary<string,GameObject> _spawnPoints = new Dictionary<string, GameObject>();
        
#pragma warning restore CS0649
        private void Awake()
        {
            
            _pathToJson = "Assets/Scenes/Levels/" + levelIndex + "/LevelData.json";
            LoadFromJason();

            foreach (var pool in _spawnData.pools)
            {
                foreach (GameObject enemy in enemyPrefabs )
                {
                    if (enemy.name == pool.enemyName)
                    {
                        _availablePools.Add(enemy.name,Instantiate(poolPrefab, gameObject.transform));
                        _availablePools[enemy.name].GetComponent<EnemyPool>().InitializePool(enemy,enemy.name,pool.size,pool.canGrow);
                    }
                }
                
            }
            
            foreach (var spawnPoint in _spawnData.points)
            {
                Vector3 pos = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z);
                
                _spawnPoints.Add(spawnPoint.pointName,Instantiate(spawnPointPrefab,pos,Quaternion.identity,gameObject.transform));
                _spawnPoints[spawnPoint.pointName].GetComponent<SpawnPoint>().InitializePoint(_availablePools, spawnPoint.pointName,spawnPoint.spawnRate,spawnPoint.pattern);
            }
        }

        [ContextMenu("SaveToJson")]
        private void SaveToJson()
        {
            var contents = JsonUtility.ToJson(_spawnData, true);
            System.IO.File.WriteAllText(_pathToJson,contents);
        }
        
        [ContextMenu("LoadFromJason")]
        private void LoadFromJason()
        {
            var contents = System.IO.File.ReadAllText(_pathToJson);
            _spawnData = JsonUtility.FromJson<SpawnData>(contents);
        }
    }
}
