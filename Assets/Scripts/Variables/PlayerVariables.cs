using System;
using UnityEngine;

namespace SelfDef.Variables
{
    [Serializable]
    [CreateAssetMenu(menuName = "Public Variables/PlayerVariable",fileName = "PlayerVariable")]
    public class PlayerVariables : ScriptableObject
    {
        [Header("Life")]
        public float maxLife;
        public float currentLife;

        [Header("Stamina")] 
        public float maxStamina;
        public float currentStamina;
        public float staminaRegenPeriod;

        [Header("Money")] 
        public int money;

        [Header("Weapon")]
        public float fireRate;

        [Header("Abilities")]
        public bool explodeOnImpact;
        public bool ultimate;

        [Header("Misc")] 
        public float headRotationSpeed;
    }
}
