using SelfDef.Systems.SpawnSystemV2.Tools;
using UnityEditor;
using UnityEngine;

// ReSharper disable InconsistentNaming
namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    [CustomEditor(typeof(EditSpawnPoints))]
    public class EditSpawnPoints_Inspector : UnityEditor.Editor
    {
        private EditSpawnPoints _target;
        private SerializedObject _serializedObject;
        private bool pointsMoved;
        private GameObject selectedObject;
        

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            _target = (EditSpawnPoints) target;
            _target.UpdateData();
            if (_target.data == null) return;

            GUILayout.Space(10);
            
            AddPositionalTools();
            
            GUILayout.Space(10);
            
            OpenEditor();
        }

        private void OpenEditor()
        {
            if (!GUILayout.Button("Open Spawn Data Editor")) return;
            
            SpawnDataOfLevel_InspectorWindow.Open(_target.data);
        }
        
        private void AddPositionalTools()
        {
            _target.UpdateData();
            _serializedObject = new SerializedObject(_target.data);
            
            GUILayout.Label("Positional Objects");
            
            EditorGUILayout.BeginHorizontal();
            {
                AddCrateButton();
                AddUpdateAllPosButton();
                AddDeleteButton();
            }
            EditorGUILayout.EndHorizontal();
        }

        private void AddCrateButton()
        {
            SpawnDataOfLevel_InspectorWindow.Open(_target.data);
            
            var PositionalObjectsExists = GameObject.Find("PositionalObjects (Temporary)");
            if (GUILayout.Button("Crate")) _target.createPositionalObjects = true;

            if (_target.createPositionalObjects)
            {
                if (PositionalObjectsExists == null)
                {
                    selectedObject = Selection.activeGameObject;
                    Debug.Log(selectedObject);
                    if(_target.autoLockInspector) ActiveEditorTracker.sharedTracker.isLocked = true;
            
                    var spawnPoints = _serializedObject.FindProperty("spawnPoints");
                    var parent = new GameObject("PositionalObjects (Temporary)");
                    parent.transform.parent = _target.gameObject.transform;
            
                    for (var i = 0; i < spawnPoints.arraySize; i++)
                    {
                        var realPoint = spawnPoints.GetArrayElementAtIndex(i);
                        var point = new GameObject(realPoint.displayName);
                        point.transform.parent = parent.transform;
                        point.transform.position = realPoint.FindPropertyRelative("spawnPointTransform").vector3Value;
                        point.transform.localScale /= 2;

                        var labelComp = point.AddComponent<DrawPointLabel>();
                        labelComp.labelText = realPoint.displayName;
                        labelComp.labelStyle = _target.labelStyle;
                        labelComp.labelOffset = _target.labelVerticalOffset;

                        var rendererComp = point.AddComponent<MeshRenderer>();
                        rendererComp.material = _target.positionItemMaterial;
                
                        var meshComp = point.AddComponent<MeshFilter>();
                        meshComp.mesh = _target.positionItemMesh;

                        var updaterComp = point.AddComponent<AutoUpdatePointPosition>();
                        updaterComp.positionName = realPoint.displayName;
                        updaterComp.spawnPointTransform = _serializedObject;
                        updaterComp.pointPosition = point.transform.position;
                        
                
                        SelectObject(point);
                    }
                }
            }
            _target.createPositionalObjects = false;
        }

        private void AddDeleteButton()
        {
            if (GUILayout.Button("Delete")) _target.deletePositionalObjects = true;

            if (_target.deletePositionalObjects)
            {
                if(_target.autoLockInspector) ActiveEditorTracker.sharedTracker.isLocked = false;
                
                SelectObject(selectedObject);

                var parent = GameObject.Find("PositionalObjects (Temporary)");
                if(parent != null) DestroyImmediate(parent);
            }

            _target.deletePositionalObjects = false;
        }
        
        private void AddUpdateAllPosButton()
        {
            _target.UpdateData();
            _serializedObject = new SerializedObject(_target.data);
            
            if (GUILayout.Button("Update Spawn Point Positions")) _target.updatePositionalObjects = true;
            
            if (_target.updatePositionalObjects)
            {
                var points = 
                    GameObject.Find("PositionalObjects (Temporary)")?.GetComponentsInChildren<Transform>() ??
                    GameObject.Find("SpawnPoints")?.GetComponentsInChildren<Transform>();
                if(points == null) return;

                var spawnPoints = _serializedObject.FindProperty("spawnPoints");

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
            
                _serializedObject.ApplyModifiedPropertiesWithoutUndo();

                if (!EditorWindow.HasOpenInstances<SpawnDataOfLevel_InspectorWindow>()) return;
            
                var window = (SpawnDataOfLevel_InspectorWindow)EditorWindow.GetWindow(typeof(SpawnDataOfLevel_InspectorWindow));
                window.Repaint();
            }

            _target.updatePositionalObjects = false;
        }
        
        private static void SelectObject(Object obj)
        {
            if(obj == null) return;
            if(Selection.activeObject.name != obj.name) Selection.activeObject = obj;
        }
    }
}
