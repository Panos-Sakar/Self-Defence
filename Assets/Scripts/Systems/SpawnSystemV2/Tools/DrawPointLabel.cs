using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SelfDef.Systems.SpawnSystemV2.Tools
{
    
    public class DrawPointLabel : MonoBehaviour
    {
        
        [HideInInspector]
        public string labelText;
        [HideInInspector]
        public GUIStyle labelStyle;
        [HideInInspector]
        public float labelOffset = 1;
        [HideInInspector]
        public Vector3 labelPosition;
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            labelPosition = gameObject.transform.position;
            Handles.Label(labelPosition + Vector3.up*labelOffset, labelText,labelStyle);
        }
#endif
    }
}
