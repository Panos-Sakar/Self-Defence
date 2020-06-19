using PlayerScripts;
using SelfDef.PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

namespace Prefabs.Projectiles.Arrow.Prefab.Scripts
{
    public class SpawnProjectileEffects : MonoBehaviour
    {
#pragma warning disable CS0649
        [Header("Properties")]
        [SerializeField] private ProjectileProperties myProperties;
        
        [Header("Attributes")]
        [SerializeField] private bool canPassThruEnemy;
        [SerializeField] private bool startsFromPlayer;
        
        [Header("Particle Effects")]
        [SerializeField]private bool hasStartEffect;
        [SerializeField] private GameObject startEffect;
        
        
        [SerializeField] private bool hasImpactEffect;
        [SerializeField] private GameObject impactEffect;
        
        
        [SerializeField] private bool hasExplodeEffect;
        [SerializeField] private GameObject explodeEffect;
        
        private Transform _myTransform;
        private Vector3 _startPos = new Vector3(0,2,0);
        

#pragma warning restore CS0649
        private void Start()
        {
            myProperties.GetComponentInParent<ProjectileProperties>();
            _myTransform = transform;

            if (!startsFromPlayer) _startPos = _myTransform.position;

            if (hasStartEffect)
            {
                Instantiate(startEffect, _startPos, Quaternion.LookRotation(_startPos, Vector3.up));
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            
            var pos = _myTransform.position;
            quaternion rot = _myTransform.rotation;
            
            if (other.CompareTag("Obstacle")) // -- Obstacle Collision -------------------------------------------------
            {
                myProperties.speed = 0;

                if (hasImpactEffect)
                {
                    Instantiate(impactEffect, pos, rot);
                }
            
                if (hasExplodeEffect && PlayerUpgrades.Instance.explodeOnImpact)
                {
                    Instantiate(explodeEffect, pos, rot);
                }

                myProperties.DestroyProjectile();
            }
            else if (other.CompareTag("Enemy")) // -- Enemy Collision --------------------------------------------------
            {
                if (!canPassThruEnemy)
                {
                    myProperties.speed = 0;
                }

                if (hasImpactEffect)
                {
                    Instantiate(impactEffect, pos, rot);
                }

                if (!canPassThruEnemy)
                {
                    myProperties.DestroyProjectile();
                }
            }
        }
    }
}
