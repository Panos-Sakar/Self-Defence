using SelfDef.Interfaces;
using UnityEngine;

namespace SelfDef.Systems.FireProjectile
{
    public class ProjectileProperties : MonoBehaviour, IChangeSetting
    {
#pragma warning disable CS0649
        
        [Header("Attributes")]
        [SerializeField] public float damage;
        [SerializeField] public float speed;
        [SerializeField] public float maxDistance;
        [SerializeField] public float expireIn;
        
        [Header("IChangeSetting")]
        [SerializeField] private bool changeLevel;
        [SerializeField] private bool toggleMasterVolume;
        
        public bool ChangeLevel 
        {
            get => changeLevel;
            set => changeLevel = value;
        }

        public bool ToggleMasterVolume
        {
            get => toggleMasterVolume;
            set => toggleMasterVolume = value;
        }
        
#pragma warning restore CS0649

        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }


    }
}
