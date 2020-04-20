using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _enemyNavMesh;
    private Transform _playerTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyNavMesh = GetComponent<NavMeshAgent>();
        _playerTransform = _player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _enemyNavMesh.destination = _playerTransform.position;
    }
}
