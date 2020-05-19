using UnityEngine;

namespace Prefabs.Projectiles.Arrow.Prefab.Scripts
{
    public class ProjectileProperties : MonoBehaviour
    {
#pragma warning disable CS0649
        
        [SerializeField] public float damage;
        [SerializeField] public float speed;
        [SerializeField] public float maxDistance;
        [SerializeField] public float expireIn;
    
#pragma warning restore CS0649

        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }
    }
}
