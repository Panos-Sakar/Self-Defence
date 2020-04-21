using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyPool : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject pinPong;
    [SerializeField] private int poolDepth = 0;
    [SerializeField] private bool canGrow = true;

    private readonly List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < poolDepth; i++)
        {
            GameObject pooledObject = Instantiate(enemyPrefab);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
        }
    }

    public GameObject GetAvailableObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                return pool[i];
            }
        }

        if (canGrow)
        {
            GameObject pooledObject = Instantiate(enemyPrefab);
            pooledObject.SetActive(false);
            pool.Add(pooledObject);
            return pooledObject;
        }
        else
        {
            return pinPong;
        }
    }
}
