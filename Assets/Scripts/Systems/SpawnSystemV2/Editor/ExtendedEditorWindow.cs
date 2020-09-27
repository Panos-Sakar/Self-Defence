using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    public class ExtendedEditorWindow : EditorWindow
    {
        protected SerializedObject SerializedObject;
        protected SerializedProperty CurrentProperty;
        
        protected static void DrawProperties(SerializedProperty properties , bool drawChildren)
        {
            var lastPropertyPath = string.Empty;

            foreach (SerializedProperty property in properties)
            {
                if (property.isArray && property.propertyType == SerializedPropertyType.Generic)
                {
                    EditorGUILayout.BeginHorizontal();
                    property.isExpanded = EditorGUILayout.Foldout(property.isExpanded, property.displayName);
                    EditorGUILayout.EndHorizontal();

                    if (property.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawProperties(property,drawChildren);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    if(!string.IsNullOrEmpty(lastPropertyPath) && property.propertyPath.Contains(lastPropertyPath)){continue;}

                    lastPropertyPath = property.propertyPath;
                    EditorGUILayout.PropertyField(property, drawChildren);
                }
            }
        }
        
        protected void DrawField(string propName, bool relative)
        {
            if (relative & CurrentProperty != null)
            {
                EditorGUILayout.PropertyField(CurrentProperty.FindPropertyRelative(propName), true);
            }
            else if (SerializedObject != null)
            {
                EditorGUILayout.PropertyField(SerializedObject.FindProperty(propName), true);
            }
        }
        
        protected void DrawFieldWithLabel(string propName, string label, int width, int labelWidth = 75, bool relative = true)
        {
            if (relative & CurrentProperty != null)
            {
                var tempWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = labelWidth;
                EditorGUILayout.PropertyField(CurrentProperty.FindPropertyRelative(propName),new GUIContent(label),true,GUILayout.Width(width),GUILayout.ExpandWidth (false));
                EditorGUIUtility.labelWidth = tempWidth;
            }
            else if (SerializedObject != null)
            {
                var tempWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = labelWidth;
                EditorGUILayout.PropertyField(SerializedObject.FindProperty(propName),new GUIContent(label),true,GUILayout.Width(width),GUILayout.ExpandWidth (false));
                EditorGUIUtility.labelWidth = tempWidth;
            }
        }
        
        protected static void DrawUiLine(Color color, int thickness = 2, int padding = 10)
        {
            var r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
            r.height = thickness;
            r.y+=padding/2f;
            r.x-=2;
            r.width +=6;
            EditorGUI.DrawRect(r, color);
        }
        
        protected static void DrawVerticalSeparator()
        {
            GUILayout.Space(10);
            DrawUiLine(Color.gray);
            GUILayout.Space(10);
        }
        
        protected void Apply()
        {
            SerializedObject.ApplyModifiedProperties();
        }
    }
}
