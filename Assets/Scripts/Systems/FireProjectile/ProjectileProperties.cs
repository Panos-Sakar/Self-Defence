using SelfDef.Interfaces;
using UnityEngine;

namespace SelfDef.Systems.FireProjectile
{
    public class ProjectileProperties : MonoBehaviour, IActivateSettings
    {
#pragma warning disable CS0649
        
        [Header("Attributes")]
        [SerializeField] public float damage;
        [SerializeField] public float speed;
        [SerializeField] public float maxDistance;
        [SerializeField] public float expireIn;
        
        [Header("IActivateSettings")]
        [SerializeField] private bool changeLevel;
        [SerializeField] private bool toggleVolume;
        [SerializeField] private bool giveUpgrade;
        
        public bool ChangeLevel 
        {
            get => changeLevel;
            set => changeLevel = value;
        }

        public bool ToggleMasterVolume
        {
            get => toggleVolume;
            set => toggleVolume = value;
        }

        public bool GiveUpgrade 
        {             
            get => giveUpgrade;
            set => giveUpgrade = value;
        }

#pragma warning restore CS0649

        public void DestroyProjectile()
        {
            Destroy(gameObject);
        }


    }
}
