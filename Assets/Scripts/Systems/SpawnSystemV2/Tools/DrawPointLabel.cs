using UnityEditor;
using UnityEngine;

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

        private void OnDrawGizmos()
        {
            labelPosition = gameObject.transform.position;
            Handles.Label(labelPosition + Vector3.up*labelOffset, labelText,labelStyle);
        }
    }
}
