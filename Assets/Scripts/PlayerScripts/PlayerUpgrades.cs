using UnityEngine;

namespace SelfDef.PlayerScripts
{
    public class PlayerUpgrades : MonoBehaviour
    {
#pragma warning disable CS0649
        public static PlayerUpgrades Instance { get; private  set; }

        public bool explodeOnImpact;
        public bool ultimate;
        
#pragma warning restore CS0649
        private void Awake()
        {
            if (Instance == null) { Instance = this; } else { Destroy(gameObject); }

            explodeOnImpact = false;
            ultimate = false;
        }
    }
}
