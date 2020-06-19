using System;
using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    // ReSharper disable once InconsistentNaming
    public class SpawnDataOfLevel_InspectorWindow : ExtendedEditorWindow
    {
        private static SpawnDataOfLevel _target;
        
        private SerializedProperty _spawnPoints;
        private SerializedProperty _enemyPools;
        public static void Open(SpawnDataOfLevel dataObject)
        {
            var window = GetWindow<SpawnDataOfLevel_InspectorWindow>("Spawn editor window");
            window.SerializedObject = new SerializedObject(dataObject);
            _target = dataObject;
        }

        private void OnSelectionChange()
        {
            Open(_target);
            
        }

        private void OnGUI()
        {
            
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
            
            _spawnPoints = SerializedObject.FindProperty("spawnPoints");
            _enemyPools = SerializedObject.FindProperty("availablePools");
            
            DrawSidebar(_spawnPoints,_enemyPools);

            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));

            if (SelectedProperty != null)
            {
                DrawProperties(SelectedProperty,true);
                
                EditorGUILayout.Space(10);
                if (GUILayout.Button("Delete Element"))
                {
                    switch (PropertyType)
                    {
                        case "Spawn Point":
                            _spawnPoints.DeleteArrayElementAtIndex(CurrentPointIndex);
                            break;
                        case "Object Pool":
                            _enemyPools.DeleteArrayElementAtIndex(CurrentPoolIndex);
                            break;
                    }
                }
            }
            else
            {
                EditorGUILayout.LabelField("Select an item form the list");
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            
            Apply();
        }
    }
}
