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
    [SerializeField] private bool destroyMe = false;
    
    // Start is called before the first frame update
    void OnEnable()
    {
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
    
    void OnCollisionEnter (Collision col)
    {
        Debug.Log(col.gameObject.name);
    }
}
