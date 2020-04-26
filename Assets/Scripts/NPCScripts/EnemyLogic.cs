using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyLogic : MonoBehaviour
{
    private GameObject _player = null;
    private NavMeshAgent _enemyNavMesh;
    private Transform _playerTransform;
    private Transform _myTransform;
    
    [SerializeField] private GameObject explosionParticle = null;
    [SerializeField] private GameObject hitParticle = null;

    [SerializeField] public float damageAmount = 0;
    [SerializeField] public float life = 1;
    [SerializeField] public float maxLife = 1;
    
    private Vector3 _particlePosition;
    private Quaternion _particleRotation;
    // Start is called before the first frame update
    void OnEnable()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyNavMesh = GetComponent<NavMeshAgent>();
        _playerTransform = _player.transform;
        _myTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
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

        if (other.gameObject.CompareTag("Projectile"))
        {
            float damageTaken = 1;
            life -= damageTaken;

            if (life <= 0)
            {
                life = maxLife;
                _particlePosition = _myTransform.position;
                _particleRotation = _myTransform.rotation;

                _player.GetComponent<PlayerLogicScript>().GiveMoney(1);
                Instantiate(hitParticle, _particlePosition, _particleRotation);
                gameObject.SetActive(false);
            }
        }
    }

    
}
