using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SpawnEnemyScript : MonoBehaviour
{
    [SerializeField] private EnemyPool _enemytPool;
    private Transform _myTransform;
    private float _randomSeed;

    // Start is called before the first frame update
    void Start()
    {
        if (_enemytPool == null)
        {
            Debug.Log("Auto assign enemy pool");
            GetComponent<EnemyPool>();
        }
        _myTransform = transform;
        _randomSeed = Random.Range(1f, 5f);
        InvokeRepeating("SpawnEnemy", 1f + _randomSeed, 3f*_randomSeed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnEnemy()
    {
        GameObject enemy = _enemytPool.GetAvailableObject();
        
        enemy.transform.position = _myTransform.position;
        enemy.SetActive(true);
        }
}
