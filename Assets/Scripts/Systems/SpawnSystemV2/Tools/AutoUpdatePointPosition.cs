using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SelfDef.Systems.SpawnSystemV2.Tools
{
    public class AutoUpdatePointPosition : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 pointPosition;
        [HideInInspector]
        public string positionName;
#if UNITY_EDITOR
        [HideInInspector]
        public SerializedObject spawnPointTransform;
#endif
    }
}
