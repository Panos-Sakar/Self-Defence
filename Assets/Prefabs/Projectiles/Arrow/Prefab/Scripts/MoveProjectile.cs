using UnityEngine;

namespace SelfDef.Prefabs.Projectiles.Arrow.Prefab.Scripts
{
    public class MoveProjectile : MonoBehaviour
    {
#pragma warning disable CS0649
    
        [SerializeField] private ProjectileProperties myProperties;


        private float _timeToSelfDestroy;
        private float _timeOfSpawn;
        private Transform _myTransform;

    
#pragma warning restore CS0649
        
        private void Start()
        {
            _myTransform = transform;
            _timeOfSpawn = Time.time;
        
            if (myProperties.speed > 0)
            {
                _timeToSelfDestroy = myProperties.maxDistance / myProperties.speed;
            }
            else
            {
                _timeToSelfDestroy = myProperties.expireIn;
            }
        }
        
        private void Update()
        {
            if (myProperties.speed > 0)
            {
                _myTransform.position += transform.forward * (Time.deltaTime * myProperties.speed);
            }


            if (Time.time - _timeOfSpawn > _timeToSelfDestroy)
            {
                myProperties.DestroyProjectile();
            }
        }
    }
}
