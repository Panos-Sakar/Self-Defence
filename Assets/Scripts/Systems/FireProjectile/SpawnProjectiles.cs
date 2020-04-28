using System.Collections.Generic;
using UnityEngine;

namespace Systems.FireProjectile
{
    public class SpawnProjectiles : MonoBehaviour
    {
#pragma warning disable CS0649
        private GameObject _projectile;
        [SerializeField] private List<GameObject> spawnEffects= new List<GameObject>();
        [SerializeField] private RotateToMouse rotateToMouse ;
        [SerializeField] private GameObject spawnPoint;

        private Transform _spawnPointTransform;
        private GameObject _projectileInstance;

#pragma warning restore CS0649
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
}
