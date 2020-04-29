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
        enum ProjectileTypeEnum
        {
            DefaultProjectile,
            SmallImpactProjectile
        };

        [SerializeField] private float speed;
        [SerializeField] private float timeToSelfDestroy;
        [SerializeField] private GameObject impactEffect;
        [SerializeField] private GameObject startEffect;
        [SerializeField] private GameObject explodeCollider;
        [SerializeField] private ProjectileTypeEnum projectileType = ProjectileTypeEnum.DefaultProjectile;
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
            if (other.CompareTag("Obstacle")) // -- Obstacle Collision -------------------------------------------------
            {
                speed = 0;
                Vector3 pos = _myTransform.position;
                quaternion rot = _myTransform.rotation;

                Instantiate(impactEffect, pos, rot);
                if (projectileType == ProjectileTypeEnum.SmallImpactProjectile && PlayerUpgrades.Instance.explodeOnImpact)
                {
                    Instantiate(explodeCollider, pos, rot);
                }

                Destroy(gameObject);
            }

            else if (other.CompareTag("Player")) // -- Player Collision ------------------------------------------------
            {
                Instantiate(startEffect, _startPos, Quaternion.LookRotation(_startPos, Vector3.up));
            }

            else if (other.CompareTag("Enemy")) // -- Enemy Collision --------------------------------------------------
            {
                EnemyLogic enemyLogic = other.GetComponent<EnemyLogic>();
                Rigidbody enemyRigidBody = other.GetComponent<Rigidbody>();
                
                speed = 0;

                Vector3 pos = _myTransform.position;
                quaternion rot = _myTransform.rotation;
                
                enemyLogic.DamageEnemy(damageAmount);
                
                switch (enemyLogic.enemyType)
                {
                    case EnemyLogic.EnemyTypeEnum.SmallBall:
                        
                        enemyRigidBody.AddForce(Vector3.up * 5);
                        
                        break;
                    
                    case EnemyLogic.EnemyTypeEnum.BigBall:
                        
                        enemyRigidBody.AddForce(Vector3.up * 100);
                        
                        break;

                    default:

                        #if UNITY_EDITOR
                            Debug.Log("Hit new enemy type");
                        #endif
                            
                        break;
                }

                switch (projectileType)
                {
                    case ProjectileTypeEnum.SmallImpactProjectile:
                        
                        Instantiate(explodeCollider, pos, rot);
                        
                        break;
                }
                
                Instantiate(impactEffect, pos, rot);
                
                
                Destroy(gameObject);
            }
        }
    }
}
