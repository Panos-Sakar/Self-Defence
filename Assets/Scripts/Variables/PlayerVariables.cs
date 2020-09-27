using System;
using System.Collections.Generic;
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
        public Dictionary<PlayerAbilities, bool> playerAbilities;

        [Header("Misc")] 
        public float headRotationSpeed;

        public int kills;
        
        public enum PlayerAbilities
        {
            None = 0,
            ExplodeOnImpact = 1,
            StarUltimate = 2
        }
    }
}
