using System;
using UnityEngine;

namespace SelfDef.Variables
{
    [Serializable]
    [CreateAssetMenu(menuName = "Public Variables/LevelVariable",fileName = "LevelVariable")]
    public class LevelVariables : ScriptableObject
    {
        [Header("Change Cubes Positions")] 
        public Vector3 changeLevelCubePos;
    }
}
