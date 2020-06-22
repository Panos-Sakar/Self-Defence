using SelfDef.Systems.SpawnSystemV2.Tools;
using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    // ReSharper disable once InconsistentNaming
    public class SpawnDataOfLevel_InspectorWindow : ExtendedEditorWindow
    {
        private SerializedProperty _spawnPoints;
        private SerializedProperty _enemyPools;

        private string _selectedPropertyPath;
        private SerializedProperty _selectedProperty;
        private string _propertyType;
        
        private  Vector2 _waveScrollPos;
        private Vector2 _sidebarScrollPos;
        
        private int _spawnPointsIndex;
        private int _currentPointIndex;

        private int _poolsIndex;
        private int _currentPoolIndex;
        
        private int _currentWaveIndex;

        private LevelSpawnData _targetAsset;
        private LevelSpawnData _tempTarget;

        public static void Open(LevelSpawnData dataObject)
        {
            var window = GetWindow<SpawnDataOfLevel_InspectorWindow>("Spawn editor window");
            window.SerializedObject = new SerializedObject(dataObject);
            window._targetAsset = dataObject;
        }

        private void OnGUI()
        {
            DrawTitlebar();
            
            if(SerializedObject == null) return;
            SerializedObject.Update();

            DrawMainWindow();
            Apply();
        }

        //Draw window Functions
        
        private void DrawTitlebar()
        {
            GUILayout.BeginHorizontal();
            var tempWidth = EditorGUIUtility.labelWidth;
            
            EditorGUIUtility.labelWidth = position.width - 700;
            EditorGUILayout.LabelField($"Editing: {SerializedObject?.targetObject.name}");
            
            EditorGUIUtility.labelWidth = 120;
            _tempTarget = (LevelSpawnData)EditorGUILayout.ObjectField(
                "Select new Asset: ", 
                _targetAsset, 
                typeof(LevelSpawnData), 
                false);
            
            EditorGUIUtility.labelWidth = tempWidth;

            if (_tempTarget != null && _targetAsset != _tempTarget)
            {
                Open(_tempTarget);
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Open Asset from Scene",GUILayout.Width(150)))
            {
                var temp = GameObject.Find("SpawnSystem")?.GetComponent<EditSpawnPoints>().data;
                if(temp!=null) Open(temp);
            }
            
            
            GUILayout.EndHorizontal();
        }
        
        private void DrawMainWindow()
        {
            EditorGUILayout.BeginHorizontal();
            
            DrawSidebar();
            DrawPropertiesPanel();
            
            EditorGUILayout.EndHorizontal();

        }
        
        private void DrawSidebar()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.MaxWidth(150), GUILayout.ExpandHeight(true));
            
            _spawnPoints = SerializedObject.FindProperty("spawnPoints");
            _enemyPools = SerializedObject.FindProperty("availablePools");

            DrawToolbar();
            
            DrawVerticalSeparator();

            _sidebarScrollPos = EditorGUILayout.BeginScrollView(_sidebarScrollPos,GUILayout.Width(225));
            {
                _spawnPointsIndex = 0;
                GUILayout.Label("Spawn Points", EditorStyles.boldLabel);
                foreach (SerializedProperty prop in _spawnPoints)
                {
                    if (GUILayout.Button(prop.displayName))
                    {
                        _currentPointIndex = _spawnPointsIndex;
                        _propertyType = "Spawn Point";

                        EditorGUI.FocusTextInControl("");
                        _selectedPropertyPath = prop.propertyPath;
                    }

                    _spawnPointsIndex++;
                }
                
                DrawUiLine(Color.gray);
                
                AddPointButton();
                
                GUILayout.Space(10); //------------------------------------------------------------------------
                
                _poolsIndex = 0;
                GUILayout.Label("Object Pools", EditorStyles.boldLabel);
                
                foreach (SerializedProperty prop in _enemyPools)
                {
                    if (GUILayout.Button(prop.displayName))
                    {
                        _currentPoolIndex = _poolsIndex;
                        _propertyType = "Object Pool";

                        EditorGUI.FocusTextInControl("");
                        _selectedPropertyPath = prop.propertyPath;
                    }

                    _poolsIndex++;
                }

                DrawUiLine(Color.gray);

                AddPoolButton();

                EditorGUILayout.EndScrollView();
            }
            
            if (!string.IsNullOrEmpty(_selectedPropertyPath))
            {
                _selectedProperty = SerializedObject.FindProperty(_selectedPropertyPath);
            }
            
            EditorGUILayout.EndVertical();
        }

        private void DrawToolbar()
        {
            GUILayout.Label("Positional Objects");
            
            EditorGUILayout.BeginHorizontal("box");
            {
                if (GUILayout.Button("Crate"))
                {
                    ActivateButtonInEditSpawnPoints(
                        GameObject.Find("SpawnSystem")?.GetComponent<EditSpawnPoints>(),
                        "Crate");
                }
                
                if (GUILayout.Button("Update positions"))
                {
                    ActivateButtonInEditSpawnPoints(
                        GameObject.Find("SpawnSystem")?.GetComponent<EditSpawnPoints>(),
                        "Update");
                }
                
                if (GUILayout.Button("Delete"))
                {
                    ActivateButtonInEditSpawnPoints(
                        GameObject.Find("SpawnSystem")?.GetComponent<EditSpawnPoints>(), 
                        "Delete");
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void ActivateButtonInEditSpawnPoints(EditSpawnPoints obj, string button)
        {
            if (obj == null) return;
            
            if (obj.data.name != _targetAsset.name)
            {
                Debug.Log("Editing Asset from another Scene");
                return;
            }
                        
            Selection.activeObject = obj;
                
            switch (button)
            {
                case "Crate":
                    obj.createPositionalObjects = true;
                    return;
                case "Update":
                    obj.updatePositionalObjects = true;
                    return;
                case "Delete":
                    obj.deletePositionalObjects = true;
                    return;
            }
        }

        private void DrawPropertiesPanel()
        {
            EditorGUILayout.BeginVertical("box", GUILayout.ExpandHeight(true));
            if (_selectedProperty != null)
            {
                switch (_selectedProperty.type)
                {
                    case "SpawnPoint":
                        DrawSpawnPointEditor();
                        break;
                    case "EnemyPool":
                        DrawEnemyPoolEditor();
                        break;
                }
            }
            else
            {
                EditorGUILayout.LabelField("Select an item form the list");
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawSpawnPointEditor()
        {
            CurrentProperty = _selectedProperty;
            
            EditorGUILayout.LabelField($"Editing Point: {CurrentProperty.displayName}");
            
            EditorGUILayout.Space(5);

            EditorGUILayout.BeginHorizontal("box");
            {
                DrawField("pointName", true);
                
                EditorGUILayout.Separator();
                
                if(AddDeleteElementButton()) return;
            }
            EditorGUILayout.EndHorizontal();
                        
            EditorGUILayout.Space(10);
                        
            EditorGUILayout.BeginHorizontal("box");
            {
                DrawField("spawnPointTransform", true);
                
                EditorGUILayout.Separator();
                
                AddUpdatePositionButton(CurrentProperty.FindPropertyRelative("spawnPointTransform"));
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(10);
                        
            EditorGUILayout.BeginVertical("box");
            {
                var waves = CurrentProperty.FindPropertyRelative("waves");
                
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.LabelField("Edit Wave Information:");
                    AddNewWaveButton(waves);
                }
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(10);
                        
                _waveScrollPos = EditorGUILayout.BeginScrollView(_waveScrollPos);
                {
                    _currentWaveIndex = 0;
                    
                    foreach (SerializedProperty wave in waves)
                    {   
                        CurrentProperty = wave;
                    
                        EditorGUILayout.BeginHorizontal();
                        {
                            EditorGUILayout.LabelField(CurrentProperty.displayName,GUILayout.Width(75));
                        
                            if(AddDeleteWaveButton(waves,_currentWaveIndex)) continue;
                        }
                        EditorGUILayout.EndHorizontal();
                    
                        EditorGUILayout.Space(5);
                            
                        EditorGUILayout.BeginHorizontal();
                        {
                            DrawFieldWithLabel("waveDelay","Start wave after ", 175,100);
                            EditorGUILayout.LabelField("     seconds.",GUILayout.Width(100));
                        }
                        EditorGUILayout.EndHorizontal();
                            
                        EditorGUILayout.BeginHorizontal();
                        {
                            DrawFieldWithLabel("size", "Spawn ",175,100);
                            DrawFieldWithLabel("enemyType","",100, 0);
                            DrawFieldWithLabel("spawnRate"," every ",120, 50);
                            EditorGUILayout.LabelField(" seconds.",GUILayout.Width(75));
                        }
                        EditorGUILayout.EndHorizontal();
                    
                        EditorGUILayout.Space(20);
                    
                        _currentWaveIndex++;
                    }
                    CurrentProperty = _selectedProperty;
                }
                EditorGUILayout.EndScrollView();
            }
            EditorGUILayout.EndVertical();
        }
        
        private void DrawEnemyPoolEditor()
        {
            CurrentProperty = _selectedProperty;
            
            EditorGUILayout.LabelField($"Editing Pool: {CurrentProperty.displayName}");
            
            EditorGUILayout.Space(5);
                        
            EditorGUILayout.BeginHorizontal("box");
            {
                DrawField("poolName", true);
                
                EditorGUILayout.Separator();
                
                if(AddDeleteElementButton()) return;
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(10);
            
            EditorGUILayout.BeginVertical("box");
            {
                EditorGUILayout.BeginHorizontal();
                {
                    DrawFieldWithLabel("enemyPrefab","Enemy Prefab",300,100 );
                    DrawFieldWithLabel("enemyType", " of type ", 200,50);  
                }
                EditorGUILayout.EndHorizontal();
                
                EditorGUILayout.Space(10);
                
                DrawFieldWithLabel("size","Size ",200,100);
                DrawFieldWithLabel("canGrow","Can Grow " ,100);
            }
            EditorGUILayout.EndVertical();
        }
        
        //Add Button Functions

        private void AddPointButton()
        {
            if (!GUILayout.Button("Add Point")) return;
            
            var point = SerializedObject.FindProperty("spawnPoints");
            point.InsertArrayElementAtIndex(point.arraySize);

            var element = point.GetArrayElementAtIndex(point.arraySize - 1);

            var propertyRelative = element.FindPropertyRelative("pointName");
            var targetName = $"new {point.arraySize - 1}";
            propertyRelative.stringValue = targetName;
            propertyRelative = element.FindPropertyRelative("spawnPointTransform");
            propertyRelative.vector3Value = Vector3.zero;
            propertyRelative = element.FindPropertyRelative("waves");
            propertyRelative.arraySize = 0;
        }

        private void AddPoolButton()
        {
            if (!GUILayout.Button("Add Pool")) return;
            
            var pool = SerializedObject.FindProperty("availablePools");
            pool.InsertArrayElementAtIndex(pool.arraySize);

            var element = pool.GetArrayElementAtIndex(pool.arraySize - 1);

            var propertyRelative = element.FindPropertyRelative("poolName");
            propertyRelative.stringValue = $"new{pool.arraySize - 1}";
            propertyRelative = element.FindPropertyRelative("enemyType");
            propertyRelative.enumValueIndex = 0;
            propertyRelative = element.FindPropertyRelative("enemyPrefab");
            propertyRelative.objectReferenceValue = null;
            propertyRelative = element.FindPropertyRelative("size");
            propertyRelative.intValue = 0;
            propertyRelative = element.FindPropertyRelative("canGrow");
            propertyRelative.boolValue = false;
        }
        
        // ReSharper disable once UnusedMember.Local
        private void AddUpdateAllPosButton()
        {
            if (!GUILayout.Button("Update Spawn Point Positions")) return;
            
            SerializedObject.Update ();
            
            var points = 
                GameObject.Find("PositionalObjects (Temporary)")?.GetComponentsInChildren<Transform>() ??
                GameObject.Find("SpawnPoints")?.GetComponentsInChildren<Transform>();
            if(points == null) return;

            var spawnPoints = SerializedObject.FindProperty("spawnPoints");

            foreach (var point in points)
            {
                for (var i = 0; i < spawnPoints.arraySize; i++)
                {
                    var spawnPoint = spawnPoints.GetArrayElementAtIndex(i);

                    if (point.name == spawnPoint.displayName)
                    {
                        spawnPoint.FindPropertyRelative("spawnPointTransform").vector3Value = point.position;
                    }
                }
            }
        }
        
        // ReSharper disable once UnusedMember.Local
        private void AddMakeObjectsButton()
        {
            
            var spawnPoints = SerializedObject.FindProperty("spawnPoints");
            
            GUILayout.Label("Positional Objects");
            
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Crate"))
            {
                var parent = new GameObject("PositionalObjects (Temporary)");
                for (var i = 0; i < spawnPoints.arraySize; i++)
                {
                    var realPoint = spawnPoints.GetArrayElementAtIndex(i);
                    var point = new GameObject(realPoint.displayName);
                    
                    point.transform.parent = parent.transform;
                    point.transform.position = realPoint.FindPropertyRelative("spawnPointTransform").vector3Value;
                }
            }
            if (GUILayout.Button("Delete"))
            {
                var parent = GameObject.Find("PositionalObjects (Temporary)");
                if(parent != null) DestroyImmediate(parent);
            }
            EditorGUILayout.EndHorizontal();
        }
        
        private bool AddDeleteWaveButton(SerializedProperty waves, int index)
        {
            if (!GUILayout.Button("Delete Wave", GUILayout.Width(100))) return false;
            
            waves.DeleteArrayElementAtIndex(index);
            _currentWaveIndex--;
            
            return waves.arraySize == index;
        }
        
        private static void AddNewWaveButton(SerializedProperty waves)
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
        
        private bool AddDeleteElementButton()
        {
            if (!GUILayout.Button("Delete Element",GUILayout.Width(150))) return false;
            
            switch (_propertyType)
            {
                case "Spawn Point":
                    _spawnPoints.DeleteArrayElementAtIndex(_currentPointIndex);
                    break;
                case "Object Pool":
                    _enemyPools.DeleteArrayElementAtIndex(_currentPoolIndex);
                    break;
            }
            return true;
        }
        
        private static void AddUpdatePositionButton(SerializedProperty transform)
        {
            if (GUILayout.Button("Get Selected Position",GUILayout.Width(150)) && Selection.activeGameObject != null)
            {
                transform.vector3Value = Selection.activeGameObject.transform.position;
            }
        }
    }
}
