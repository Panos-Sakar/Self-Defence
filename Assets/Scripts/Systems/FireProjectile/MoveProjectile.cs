using System.Collections;
using NPCScripts;
using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.FireProjectile
{
    public class MoveProjectile : MonoBehaviour
    {
#pragma warning disable CS0649
        enum projectileTypeEnum
        {
            SmallImpactProjectile,
        };

        [SerializeField] private float speed;
        [SerializeField] private float timeToSelfDestroy;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private GameObject startEffect;
        [SerializeField] private GameObject explodeCollider;
        [SerializeField] private projectileTypeEnum projectileType = projectileTypeEnum.SmallImpactProjectile;
        private Transform _myTransform;
        private Vector3 _startPos;
    

        [SerializeField] public float damageAmount = 1;

#pragma warning restore CS0649
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
                if (projectileType == projectileTypeEnum.SmallImpactProjectile && PlayerUpgrades.Instance.explodeOnImpact)
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
}
