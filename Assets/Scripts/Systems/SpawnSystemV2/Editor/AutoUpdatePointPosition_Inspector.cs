using SelfDef.Systems.SpawnSystemV2.Tools;
using UnityEditor;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    // ReSharper disable once InconsistentNaming
    [CustomEditor(typeof(AutoUpdatePointPosition))]
    public class AutoUpdatePointPosition_Inspector : UnityEditor.Editor
    {
        
        private Tool _lastTool = Tool.None;
        private AutoUpdatePointPosition _targetPoint;
        private SpawnDataOfLevel_InspectorWindow _window;
        private Vector3 _newPosition;

        private bool _toolActive;

#pragma warning disable 414
        private bool _positionChanged;
#pragma warning restore 414
        public void OnSceneGUI()
        {
            _targetPoint = (target as AutoUpdatePointPosition);
            if (_targetPoint == null || !_toolActive) return;
#if UNITY_EDITOR           
            var newEvent = Event.current;

            if (_positionChanged 
                && newEvent.type == EventType.MouseUp 
                && newEvent.button == 0 
                && _targetPoint.spawnPointTransform!=null)
            {
                Undo.RecordObject(target, $"Changed {_targetPoint.positionName}'s Position");
                _targetPoint.pointPosition = _newPosition;
                
                var realPoints = _targetPoint.spawnPointTransform.FindProperty("spawnPoints");
                foreach (SerializedProperty realPoint in realPoints)
                {
                    if (realPoint.displayName != _targetPoint.positionName) continue;
                
                    realPoint.FindPropertyRelative("spawnPointTransform").vector3Value = _newPosition;
                    Debug.Log($"Updated {_targetPoint.positionName}'s Position");
                    break;
                }
                
                _targetPoint.spawnPointTransform.ApplyModifiedProperties();
                
                if(_window != null) _window.Repaint();
                
                _positionChanged = false;
            }
#endif
            EditorGUI.BeginChangeCheck();
            
            _newPosition = Handles.PositionHandle(_targetPoint.transform.position,Quaternion.identity);

            if (!EditorGUI.EndChangeCheck()) return;
            
            _positionChanged = true;
            _targetPoint.transform.position = _newPosition;
            
        }
        
        private void OnEnable()
        { 
            _targetPoint = (target as AutoUpdatePointPosition);
            _lastTool = UnityEditor.Tools.current;
            UnityEditor.Tools.current = Tool.None;
            _toolActive = true;
            Undo.undoRedoPerformed += UndoCallback;
            
            if (!EditorWindow.HasOpenInstances<SpawnDataOfLevel_InspectorWindow>()) return;
            
            _window = (SpawnDataOfLevel_InspectorWindow)EditorWindow.GetWindow(typeof(SpawnDataOfLevel_InspectorWindow));
            if (_targetPoint == null) return;
            _window.EditNewPoint(_targetPoint.positionName);
            
        }

        private void OnDisable()
        {
            UnityEditor.Tools.current = _lastTool;
            _toolActive = false;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Tool settings");
            
            EditorGUILayout.BeginHorizontal("box");
            {
                EditorGUILayout.LabelField($"Previous tool: {_lastTool}");
                if (GUILayout.Button("Reset tool"))
                {
                    _toolActive = false;
                    UnityEditor.Tools.current = _lastTool == Tool.None ? Tool.Move : _lastTool;
                }
            
                if (GUILayout.Button("Activate Tool"))
                {
                    _toolActive = true;
                    if (_lastTool != Tool.None)
                    {
                        _lastTool = UnityEditor.Tools.current;
                        UnityEditor.Tools.current = Tool.None;
                    }
                } 
            }
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.Space(10);
            
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Delete all positional objects"))
                {
                    var spawnSystemObj = GameObject.Find("SpawnSystem");
                    if(spawnSystemObj == null) return;
                    
                    if(Selection.activeObject.name != spawnSystemObj.name) Selection.activeObject = spawnSystemObj;
                    
                    
                    var positionalObjsParent = GameObject.Find("PositionalObjects (Temporary)");
                    if(positionalObjsParent != null) DestroyImmediate(positionalObjsParent);
                } 
            }
            EditorGUILayout.EndHorizontal();
        }

        private void UndoCallback()
        {
            if(_targetPoint!=null) _targetPoint.gameObject.transform.position = _targetPoint.pointPosition;
        }

    }
}
