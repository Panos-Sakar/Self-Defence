using UnityEngine;

namespace Prefabs.Projectiles.Arrow.Prefab
{
    public class ProjectileProperties : MonoBehaviour
    {
#pragma warning disable CS0649
        public enum ProjectileTypeEnum
        {
            DefaultProjectile,
            SmallExplodingProjectile,
            BigFastProjectile,
            Explosion
        };
    
        [SerializeField] public ProjectileTypeEnum projectileType = ProjectileTypeEnum.DefaultProjectile;
    
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
