using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Tools
{
    public class DrawPointLabel : MonoBehaviour
    {
        public string labelText;
        public GUIStyle labelStyle;
        public float labelOffset = 1;
        public Vector3 labelPosition;

        private void OnDrawGizmos()
        {
            labelPosition = gameObject.transform.position;
            Handles.Label(labelPosition + Vector3.up*labelOffset, labelText,labelStyle);
        }
    }
}
