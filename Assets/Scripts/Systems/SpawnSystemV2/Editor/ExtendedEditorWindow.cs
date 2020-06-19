using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    public class ExtendedEditorWindow : EditorWindow
    {
        protected SerializedObject SerializedObject;
        // ReSharper disable once MemberCanBePrivate.Global
        // ReSharper disable once UnassignedField.Global
        protected SerializedProperty CurrentProperty;

        private string _selectedPropertyPath;
        protected SerializedProperty SelectedProperty;


        private int _spawnPointsIndex;
        protected int CurrentPointIndex;

        private int _poolsIndex;
        protected int CurrentPoolIndex;
        
        protected string PropertyType;

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

        protected void DrawSidebar(SerializedProperty spawnPoints, SerializedProperty availablePools)
        {
            _spawnPointsIndex = 0;
            GUILayout.Label("Spawn Points",EditorStyles.boldLabel);
            foreach (SerializedProperty prop in spawnPoints)
            {
                if (GUILayout.Button(prop.displayName))
                {
                    CurrentPointIndex = _spawnPointsIndex;
                    PropertyType = "Spawn Point";
                    
                    EditorGUI.FocusTextInControl("");
                    _selectedPropertyPath = prop.propertyPath;
                }
                _spawnPointsIndex++;
            }
            
            DrawUiLine(Color.gray);
            if (Selection.activeObject == null)
            {
                if (GUILayout.Button("Add Point"))
                {
                    var point = SerializedObject.FindProperty("spawnPoints");
                        point.InsertArrayElementAtIndex(point.arraySize);

                        var element = point.GetArrayElementAtIndex(point.arraySize - 1);

                        var propertyRelative = element.FindPropertyRelative("pointName");
                        propertyRelative.stringValue = $"new{point.arraySize - 1}";
                        propertyRelative = element.FindPropertyRelative("spawnPointTransform");
                
                        propertyRelative.vector3Value = Vector3.zero;
                }
            }
            else
            {
                if (GUILayout.Button("Add Points from Selection"))
                {
                    var point = SerializedObject.FindProperty("spawnPoints");
                    point.InsertArrayElementAtIndex(point.arraySize);

                    var element = point.GetArrayElementAtIndex(point.arraySize - 1);

                    var propertyRelative = element.FindPropertyRelative("pointName");
                    propertyRelative.stringValue = $"new{point.arraySize - 1}";
                    propertyRelative = element.FindPropertyRelative("spawnPointTransform");
                
                    propertyRelative.vector3Value = Selection.activeGameObject.transform.position;
                }  
            }


            GUILayout.Space(10); //------------------------------------------------------------------------
            
            _poolsIndex = 0;
            GUILayout.Label("Object Pools",EditorStyles.boldLabel);
            foreach (SerializedProperty prop in availablePools)
            {
                if (GUILayout.Button(prop.displayName))
                {
                    CurrentPoolIndex = _poolsIndex;
                    PropertyType = "Object Pool";
                    
                    EditorGUI.FocusTextInControl("");
                    _selectedPropertyPath = prop.propertyPath;
                }
                _poolsIndex++;
            }

            DrawUiLine(Color.gray);
            if(GUILayout.Button("Add Pool"))
            {
                var pool  = SerializedObject.FindProperty("availablePools");
                pool.InsertArrayElementAtIndex(pool.arraySize);
                
                var element = pool.GetArrayElementAtIndex(pool.arraySize-1);
                
                var propertyRelative = element.FindPropertyRelative("poolName");
                propertyRelative.stringValue = $"new{pool.arraySize-1}";
                propertyRelative = element.FindPropertyRelative("enemyPrefab");
                propertyRelative.objectReferenceValue = null;
                propertyRelative = element.FindPropertyRelative("size");
                propertyRelative.intValue = 0;
                propertyRelative = element.FindPropertyRelative("canGrow");
                propertyRelative.boolValue = false;

            }

            if (!string.IsNullOrEmpty(_selectedPropertyPath))
            {
                SelectedProperty = SerializedObject.FindProperty(_selectedPropertyPath);
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
        
        private static void DrawUiLine(Color color, int thickness = 2, int padding = 10)
        {
            Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
            r.height = thickness;
            r.y+=padding/2f;
            r.x-=2;
            r.width +=6;
            EditorGUI.DrawRect(r, color);
        }


        protected void Apply()
        {
            SerializedObject.ApplyModifiedProperties();
        }
    }
}
