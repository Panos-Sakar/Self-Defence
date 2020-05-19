using PlayerScripts;
using Unity.Mathematics;
using UnityEngine;

namespace Prefabs.Projectiles.Arrow.Prefab
{
    public class SpawnProjectileEffects : MonoBehaviour
    {
#pragma warning disable CS0649
    
        [SerializeField] private ProjectileProperties myProperties;

        [SerializeField] private GameObject impactEffect;
        [SerializeField] private GameObject startEffect;
        [SerializeField] private GameObject explodeEffect;
        [SerializeField] private bool canPassThruEnemy;
    
    
        private Transform _myTransform;
        private Vector3 _startPos;

        private bool _hasStartEffect;
        private bool _hasImpactEffect;
        private bool _hasExplodeEffect;

#pragma warning restore CS0649
        private void Start()
        {
            myProperties.GetComponentInParent<ProjectileProperties>();
            _myTransform = transform;
            _startPos = _myTransform.position;
        
            switch(myProperties.projectileType)
            {
                case ProjectileProperties.ProjectileTypeEnum.DefaultProjectile:
                    _hasStartEffect = true;
                    _hasImpactEffect = false;
                    _hasExplodeEffect = false;
                    break;
            
                case ProjectileProperties.ProjectileTypeEnum.SmallExplodingProjectile:
                    _hasStartEffect = true;
                    _hasImpactEffect = true;
                    _hasExplodeEffect = true;
                    break;
            
            
                case ProjectileProperties.ProjectileTypeEnum.Explosion:
                    _hasStartEffect = true;
                    _hasImpactEffect = false;
                    _hasExplodeEffect = false;
                    break;
                
                case ProjectileProperties.ProjectileTypeEnum.BigFastProjectile:
                    _hasStartEffect = false;
                    _hasImpactEffect = true;
                    _hasExplodeEffect = false;
                    break;
            
                default:
                    _hasStartEffect = false;
                    _hasImpactEffect = false;
                    _hasExplodeEffect = false;
                    break;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle")) // -- Obstacle Collision -------------------------------------------------
            {
                myProperties.speed = 0;
                Vector3 pos = _myTransform.position;
                quaternion rot = _myTransform.rotation;
                
                if (_hasImpactEffect)
                {
                    Instantiate(impactEffect, pos, rot);
                }
            
                if (_hasExplodeEffect && PlayerUpgrades.Instance.explodeOnImpact)
                {
                    Instantiate(explodeEffect, pos, rot);
                }

                myProperties.DestroyProjectile();
            }
            else if (other.CompareTag("Player")) // -- Player Collision ------------------------------------------------
            {
                if (_hasStartEffect)
                {
                    Instantiate(startEffect, _startPos, Quaternion.LookRotation(_startPos, Vector3.up));
                }
            
            }
            else if (other.CompareTag("Enemy")) // -- Enemy Collision --------------------------------------------------
            {
                if (!canPassThruEnemy)
                {
                    myProperties.speed = 0;
                }

                if (_hasImpactEffect)
                {
                    Vector3 pos = _myTransform.position;
                    quaternion rot = _myTransform.rotation;
                    
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
