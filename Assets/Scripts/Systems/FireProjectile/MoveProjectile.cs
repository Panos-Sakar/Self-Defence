using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MoveProjectile : MonoBehaviour
{
    enum projectileTypeEnum // your custom enumeration
    {
        SmallImpactProjectile,
        BigImpactProjectile
    };

    [SerializeField] private float speed = 0;
    [SerializeField] private float timeToSelfDestroy = 10;
    [SerializeField] private GameObject impactEffect = null;
    [SerializeField] private GameObject startEffect = null;
    [SerializeField] private GameObject explodeCollider;
    [SerializeField] private projectileTypeEnum projectileType;
    private Transform _myTransform;
    private Vector3 _startPos;
    

    [SerializeField] public float damageAmount = 1;

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _startPos = _myTransform.position;
        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        if (speed > 0)
        {
            _myTransform.position += transform.forward * (Time.deltaTime * speed);
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSecondsRealtime(timeToSelfDestroy);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            speed = 0;
            Vector3 pos = _myTransform.position;
            quaternion rot = _myTransform.rotation;

            Instantiate(impactEffect, pos, rot);
            if (projectileType == projectileTypeEnum.SmallImpactProjectile)
            {
                Instantiate(explodeCollider, pos, rot); 
            }

            
            Destroy(gameObject);
        }
        else if (other.CompareTag("Player"))
        {
            Instantiate(startEffect, _startPos,  Quaternion.LookRotation(_startPos, Vector3.up));
        }
        else if (other.CompareTag("Enemy") && projectileType == projectileTypeEnum.SmallImpactProjectile)
        {
            speed = 0;
            Vector3 pos = _myTransform.position;
            quaternion rot = _myTransform.rotation;

            Instantiate(impactEffect, pos, rot);
            Instantiate(explodeCollider, pos, rot);
            
            Destroy(gameObject);
        }
    }
}
