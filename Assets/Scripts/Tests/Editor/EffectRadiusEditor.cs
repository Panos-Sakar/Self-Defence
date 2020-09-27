using UnityEngine;
using UnityEditor;

namespace SelfDef.Tests.Editor
{
    [CustomEditor(typeof(EffectRadius))]
    public class EffectRadiusEditor : UnityEditor.Editor
    {
        public void OnSceneGUI()
        {
            var t = (target as EffectRadius);
            
            if(t == null) return;
            
            EditorGUI.BeginChangeCheck();
            
            var areaOfEffect = Handles.RadiusHandle(
                Quaternion.identity, 
                t.transform.position, 
                t.areaOfEffect);

            if (!EditorGUI.EndChangeCheck()) return;

            Undo.RecordObject(target, "Changed Area Of Effect");
            t.areaOfEffect = areaOfEffect;
        }
    }
}
