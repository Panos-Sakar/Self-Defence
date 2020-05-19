using UnityEngine;

namespace Systems.FireProjectile
{
    public class SpawnProjectiles : MonoBehaviour
    {
#pragma warning disable CS0649
        [SerializeField] private GameObject mainProjectile;
        [SerializeField] private GameObject spawnPoint;

        private RotateToMouse _rotateToMouse ;
        private Transform _spawnPointTransform;
        private GameObject _projectileInstance;

#pragma warning restore CS0649
        // Start is called before the first frame update
        void Start()
        {
            _spawnPointTransform = spawnPoint.transform;
            _rotateToMouse = gameObject.GetComponentInParent<RotateToMouse>();
        }

        public void SpawnFireEffect()
        {
            _projectileInstance = Instantiate(mainProjectile, _spawnPointTransform.position, _spawnPointTransform.rotation);
            _projectileInstance.transform.localRotation = _rotateToMouse.GetRotation();
        }
    }
}
