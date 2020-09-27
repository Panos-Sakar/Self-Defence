using SelfDef.Systems.Loading;
using UnityEditor;
using UnityEngine;

namespace SelfDef.Tools.Editor
{
    [InitializeOnLoad]
    public static class PlayStateNotifier
    {
              
        static PlayStateNotifier()
        {
            EditorApplication.playModeStateChanged += ModeChanged;
        }

        private static void ModeChanged(PlayModeStateChange playModeState)
        {
            if (playModeState == PlayModeStateChange.EnteredEditMode)
            {
                var loadingHandler = GameObject.Find("LoadingHandler")?.GetComponent<LoadingHandler>();
                if(loadingHandler!=null) loadingHandler.loadMenu = true;
            
                var initializers = Resources.FindObjectsOfTypeAll<Initializer>();
                foreach (var initializer in initializers)
                {
                    initializer.gameObject.SetActive(true);
                }

                var destroyOnEditModes = GameObject.FindGameObjectsWithTag("DestroyOnEditMode");
                foreach (var destroyOnEditMode in destroyOnEditModes)
                {
                    Object.DestroyImmediate(destroyOnEditMode);
                }
            }
        }
    }
}
