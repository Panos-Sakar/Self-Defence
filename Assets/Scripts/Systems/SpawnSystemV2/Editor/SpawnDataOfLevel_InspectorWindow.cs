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
        
        private int _currentWaveIndex;
        private  Vector2 _waveScrollPos;
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
                
                switch (SelectedProperty.type)
                {
                    case "SpawnPoint":
                        CurrentProperty = SelectedProperty;
                        EditorGUILayout.LabelField("Edit Point: ");
                        EditorGUILayout.Space(5);

                        EditorGUILayout.BeginHorizontal("box");
                    {
                        DrawField("pointName", true);
                        EditorGUILayout.Separator();
                        AddDeleteButton();
                    }
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.Space(10);
                        
                        EditorGUILayout.BeginHorizontal("box");
                    {
                        DrawField("spawnPointTransform", true);
                        EditorGUILayout.Separator();
                        AddUpdateButton(CurrentProperty.FindPropertyRelative("spawnPointTransform"));
                    }
                        EditorGUILayout.EndHorizontal();

                        EditorGUILayout.Space(10);
                        
                        EditorGUILayout.BeginVertical("box");
                    {
                        var waves = CurrentProperty.FindPropertyRelative("waves");
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField("Edit Wave Information:");
                        AddWaveButton(waves);
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.Space(10);
                        
                        _waveScrollPos = EditorGUILayout.BeginScrollView(_waveScrollPos);
                        
                        _currentWaveIndex = 0;
                            
                        foreach (SerializedProperty wave in waves)
                        {   
                            CurrentProperty = wave;
                            EditorGUILayout.BeginHorizontal();
                            EditorGUILayout.LabelField(CurrentProperty.displayName,GUILayout.Width(75));
                            if(AddDeleteWaveButton(waves,_currentWaveIndex)) continue;
                            
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.Space(5);
                            
                            EditorGUILayout.BeginHorizontal();
                            DrawFieldWithLabel("waveDelay","Start wave after ", 175,100);
                            EditorGUILayout.LabelField("     seconds.",GUILayout.Width(100));
                            EditorGUILayout.EndHorizontal();
                            
                            EditorGUILayout.BeginHorizontal();
                            
                            DrawFieldWithLabel("size", "Spawn ",175,100);
                            DrawFieldWithLabel("enemyType","",100, 0);
                            DrawFieldWithLabel("spawnRate"," every ",120, 50);
                            EditorGUILayout.LabelField(" seconds.",GUILayout.Width(75));
                            
                            EditorGUILayout.EndHorizontal();
                            EditorGUILayout.Space(20);

                            _currentWaveIndex++;
                        }
                        CurrentProperty = SelectedProperty;
                        EditorGUILayout.EndScrollView();
                    }
                        EditorGUILayout.EndVertical();
                        break;
                    
                    case "EnemyPool":
                        DrawProperties(SelectedProperty,true);
                        break;
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

        private bool AddDeleteWaveButton(SerializedProperty waves, int index)
        {
            if (GUILayout.Button("Delete Wave",GUILayout.Width(100)))
            {
                waves.DeleteArrayElementAtIndex(index);
                _currentWaveIndex--;
                if (waves.arraySize == index)
                {
                    return true;
                }
            }
            return false;
        }
        
        private void AddWaveButton(SerializedProperty waves)
        {
            if (GUILayout.Button("Add new Wave",GUILayout.Width(100)))
            {
                waves.InsertArrayElementAtIndex(waves.arraySize);

                var element = waves.GetArrayElementAtIndex(waves.arraySize-1);
                var pos = int.Parse(element.displayName.TrimStart('W','a','v','e',' ','>'));
                var propertyRelative = element.FindPropertyRelative("waveName");
                propertyRelative.stringValue = $"Wave > {pos + 1}";
                propertyRelative = element.FindPropertyRelative("waveDelay");
                propertyRelative.floatValue = 0f;
                propertyRelative = element.FindPropertyRelative("size");
                propertyRelative.intValue = 0;
                propertyRelative = element.FindPropertyRelative("enemyType");
                propertyRelative.enumValueIndex = 0;
                propertyRelative = element.FindPropertyRelative("spawnRate");
                propertyRelative.floatValue = 0f;

            }
        }
        
        private void AddDeleteButton()
        {
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
        
        private void AddUpdateButton(SerializedProperty transform)
        {
            if (GUILayout.Button("Get Position") && Selection.activeGameObject != null)
            {
                transform.vector3Value = Selection.activeGameObject.transform.position;
            }
        }
    }
}
