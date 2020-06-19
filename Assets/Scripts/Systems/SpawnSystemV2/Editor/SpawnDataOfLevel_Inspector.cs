using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace SelfDef.Systems.SpawnSystemV2.Editor
{
    public static class AssetHandler
    {
        [OnOpenAsset]
        public static bool OpenEditor(int instanceId, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceId) as SpawnDataOfLevel;

            if (obj != null)
            {
                SpawnDataOfLevel_InspectorWindow.Open(obj);
            }
            
            return false;
        }
    }
    

    // ReSharper disable once InconsistentNaming
    [CustomEditor(typeof(SpawnDataOfLevel))]
    public class SpawnDataOfLevel_Inspector : UnityEditor.Editor
    {

        [SerializeField] private bool showInspector;
        public override void OnInspectorGUI()
        {
            
            if (GUILayout.Button("Open Editor"))
            {
                SpawnDataOfLevel_InspectorWindow.Open((SpawnDataOfLevel)target);
            }
            if (GUILayout.Button("Toggle Default Inspector"))
            {
                showInspector = !showInspector;
            }

            if (showInspector)
            {
                base.OnInspectorGUI();
            }
        }


    }
}
