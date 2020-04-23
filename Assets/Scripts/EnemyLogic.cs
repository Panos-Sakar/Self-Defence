using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyLogic : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _enemyNavMesh;
    private Transform _playerTransform;
    private Transform _myTransform;
    [SerializeField] private bool destroyMe = false;
    [SerializeField] private GameObject explosionParticle;

    private Vector3 _particlePosition;
    private Quaternion _particleRotation;
    // Start is called before the first frame update
    void OnEnable()
    {
        _myTransform = transform;
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyNavMesh = GetComponent<NavMeshAgent>();
        _playerTransform = _player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space") || destroyMe)
        {
            destroyMe = false;
            gameObject.SetActive(false);
        }

        if (gameObject.activeInHierarchy.Equals(true))
        {
            _enemyNavMesh.destination = _playerTransform.position;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _particlePosition = _myTransform.position;
            _particleRotation = _myTransform.rotation;
            Instantiate(explosionParticle, _particlePosition, _particleRotation);
            gameObject.SetActive(false);  
        }
    }

    
}
