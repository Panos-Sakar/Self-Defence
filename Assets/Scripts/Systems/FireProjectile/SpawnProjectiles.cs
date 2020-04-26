using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectiles : MonoBehaviour
{
    private GameObject _projectile;
    [SerializeField] private List<GameObject> spawnEffects= new List<GameObject>();
    [SerializeField] private RotateToMouse rotateToMouse = null;
    [SerializeField] private GameObject spawnPoint = null;

    private Transform _spawnPointTransform;
    private GameObject _projectileInstance;

    // Start is called before the first frame update
    void Start()
    {
        _projectile = spawnEffects[0];
        _spawnPointTransform = spawnPoint.transform;
    }

    public void SpawnFireEffect()
    {
        _projectileInstance = Instantiate(_projectile, _spawnPointTransform.position, _spawnPointTransform.rotation);
        _projectileInstance.transform.localRotation = rotateToMouse.GetRotation();
    }
}
