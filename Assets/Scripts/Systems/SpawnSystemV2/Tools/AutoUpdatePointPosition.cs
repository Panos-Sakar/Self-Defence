using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Tools
{
    public class AutoUpdatePointPosition : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 pointPosition;
        [HideInInspector]
        public string positionName;
        [HideInInspector]
        public SerializedObject spawnPointTransform;
    }
}
