using SelfDef.Systems.Loading;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityToolbarExtender.Examples
{
	internal static class ToolbarStyles
	{
		public static readonly GUIStyle CommandButtonStyle;

		static ToolbarStyles()
		{
			CommandButtonStyle = new GUIStyle("Command")
			{
				fontSize = 16,
				alignment = TextAnchor.MiddleCenter,
				imagePosition = ImagePosition.ImageAbove,
				fontStyle = FontStyle.Bold
			};
		}
	}

	[InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		static SceneSwitchLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("A", "Load all levels"), ToolbarStyles.CommandButtonStyle))
			{
				SceneHelper.StartScene("StartScene");
			}
		}
	}

	internal static class SceneHelper
	{
		private static string _sceneToOpen;

		public static void StartScene(string sceneName)
		{
			if(EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			_sceneToOpen = sceneName;
			EditorApplication.update += OnUpdate;
		}

		private static void OnUpdate()
		{
			if (_sceneToOpen == null ||
			    EditorApplication.isPlaying || EditorApplication.isPaused ||
			    EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			// ReSharper disable once DelegateSubtraction
			EditorApplication.update -= OnUpdate;

			if(EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
			{
				EditorSceneManager.OpenScene("Assets/Scenes/MainScene/MainScene.unity",OpenSceneMode.Additive);
				SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
				
				//TODO: rename Handler object
				var loadingHandler = GameObject.Find("LoadingHandller").GetComponent<LoadingHandler>();

				loadingHandler.loadMenu = false;
				loadingHandler.activeLevelIndex = 2;
				
				var init = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Systems/Initializer.prefab",typeof(Initializer));
				
				var initInstance = PrefabUtility.InstantiatePrefab(init);
				initInstance.name = "AutoCreatedInitializer (Destroy After PlayMode)";
				
				var instance = GameObject.Find("AutoCreatedInitializer (Destroy After PlayMode)");
				instance.tag = "DestroyOnEditMode";

				EditorApplication.isPlaying = true;
			}
			_sceneToOpen = null;
		}
	}
}