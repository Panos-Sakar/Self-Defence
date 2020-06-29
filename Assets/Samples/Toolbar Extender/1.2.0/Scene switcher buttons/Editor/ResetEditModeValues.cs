using SelfDef.Systems.Loading;
using UnityEngine;
using UnityEditor;
          
[InitializeOnLoad]
public static class PlayStateNotifier
{
              
    static PlayStateNotifier()
    {
        EditorApplication.playModeStateChanged += ModeChanged;
    }
          
    static void ModeChanged(PlayModeStateChange playModeState)
    {
        if (playModeState == PlayModeStateChange.EnteredEditMode) 
        {
            var loadingHandler = GameObject.Find("LoadingHandller").GetComponent<LoadingHandler>();
            loadingHandler.loadMenu = true;
            
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
