using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    // ReSharper disable once InconsistentNaming
    public class SpawnDataOfLevel_InspectorWindow : ExtendedEditorWindow
    {
        private SerializedProperty _spawnPoints;
        private SerializedProperty _enemyPools;
        
        private int _currentWaveIndex;
        private  Vector2 _waveScrollPos;
        public static void Open(LevelSpawnData dataObject)
        {
            var window = GetWindow<SpawnDataOfLevel_InspectorWindow>("Spawn editor window");
            window.SerializedObject = new SerializedObject(dataObject);
        }
        

        private void OnGUI()
        {
            if(SerializedObject == null) return;
            
            SerializedObject.Update();
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
                        if(AddDeleteButton()) break;
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
                        CurrentProperty = SelectedProperty;
                        
                        EditorGUILayout.LabelField("Edit Pool: ");
                        EditorGUILayout.Space(5);
                        
                        EditorGUILayout.BeginHorizontal("box");
                    {
                        DrawField("poolName", true);
                        EditorGUILayout.Separator();
                        if(AddDeleteButton()) break;
                        
                    }
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.Space(10);
                        
                        EditorGUILayout.BeginVertical("box");
                    {
                        EditorGUILayout.BeginHorizontal();
                        
                        DrawFieldWithLabel("enemyPrefab","Enemy Prefab",300,100 );
                        DrawFieldWithLabel("enemyType", " of type ", 200,50);
                        
                        EditorGUILayout.EndHorizontal();
                        
                        EditorGUILayout.Space(10);
                        
                        DrawFieldWithLabel("size","Size ",200,100);
                        DrawFieldWithLabel("canGrow","Can Grow " ,100);
                    }
                        EditorGUILayout.EndVertical();

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
            if (!GUILayout.Button("Delete Wave", GUILayout.Width(100))) return false;
            
            waves.DeleteArrayElementAtIndex(index);
            _currentWaveIndex--;
            
            return waves.arraySize == index;
        }
        
        private static void AddWaveButton(SerializedProperty waves)
        {
            if (!GUILayout.Button("Add new Wave", GUILayout.Width(100))) return;
            
            waves.InsertArrayElementAtIndex(waves.arraySize);

            var element = waves.GetArrayElementAtIndex(waves.arraySize-1);
            
            var pos = waves.arraySize == 1 ? 0 : int.Parse(element.displayName.TrimStart('W','a','v','e',' ','>'));

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
        
        private bool AddDeleteButton()
        {
            if (!GUILayout.Button("Delete Element")) return false;
            
            switch (PropertyType)
            {
                case "Spawn Point":
                    _spawnPoints.DeleteArrayElementAtIndex(CurrentPointIndex);
                    break;
                case "Object Pool":
                    _enemyPools.DeleteArrayElementAtIndex(CurrentPoolIndex);
                    break;
            }
            return true;
        }

        
        private static void AddUpdateButton(SerializedProperty transform)
        {
            if (GUILayout.Button("Get Selected Position") && Selection.activeGameObject != null)
            {
                transform.vector3Value = Selection.activeGameObject.transform.position;
            }
        }
    }
}
