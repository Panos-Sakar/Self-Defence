using UnityEngine;

namespace Systems.FireProjectile
{
    public class SpawnProjectiles : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField] private GameObject spawnPoint;
        
        [Header("Projectiles")]
        [SerializeField] private GameObject mainProjectile;
        [SerializeField] private GameObject ultimateProjectile;

        private RotateToMouse _rotateToMouseComponent;
        private Transform _spawnPointTransform;
        private GameObject _projectileInstance;

#pragma warning restore CS0649

        private void Start()
        {
            _spawnPointTransform = spawnPoint.transform;
            _rotateToMouseComponent = gameObject.GetComponentInParent<RotateToMouse>();
        }

        public void SpawnFireEffect()
        {
            _projectileInstance = Instantiate(mainProjectile, _spawnPointTransform.position, _spawnPointTransform.rotation);
            _projectileInstance.transform.localRotation = _rotateToMouseComponent.GetRotation();
        }
        
        public void SpawnUltimateEffect(Transform playerTransform)
        {
            var pos = playerTransform.position + new Vector3(0, 2, 0);
            var forward =playerTransform.forward;
            var right = playerTransform.right;
                
            Vector3[] position = {
                forward,
                right,
                -forward,
                -right
            };

            for (int i = 0; i < 4; i++)
            {
                Instantiate(ultimateProjectile, pos, Quaternion.LookRotation (position[i]));
            }
        }
    }
}
