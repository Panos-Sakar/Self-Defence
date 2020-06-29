using SelfDef.Systems.Loading;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace SelfDef.Tools.Editor
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

		private static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("A", "Load All Levels"), ToolbarStyles.CommandButtonStyle))
			{
				SceneHelper.LoadAllLevels();
			}
			if(GUILayout.Button(new GUIContent("MS", "Load Main Scene And Play"), ToolbarStyles.CommandButtonStyle))
			{
				SceneHelper.LoadMainSceneAndPlay();
			}
		}
	}

	internal static class SceneHelper
	{
		private static string _sceneToOpen;
		private static bool _loadMainSceneAndPlay;
		private static bool _loadAllScene;

		public static void LoadMainSceneAndPlay()
		{
			if(EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			_loadMainSceneAndPlay = true;
			
			EditorApplication.update += OnUpdate;
		}
		
		public static void LoadAllLevels()
		{
			if(EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			_loadAllScene = true;
			
			EditorApplication.update += OnUpdate;
		}

		private static void OnUpdate()
		{
			if (EditorApplication.isPlaying || EditorApplication.isPaused ||
			    EditorApplication.isCompiling || EditorApplication.isPlayingOrWillChangePlaymode)
			{
				return;
			}

			// ReSharper disable once DelegateSubtraction 
			EditorApplication.update -= OnUpdate;
			
			if (_loadMainSceneAndPlay)
			{
				if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
				
				AddMainScene(true, OpenSceneMode.Additive);

				DisableMenuLevelLoading();

				CreateInitializerGameObject();
				
				_loadMainSceneAndPlay = false;
				EditorApplication.isPlaying = true;
			}
			else if (_loadAllScene)
			{
				AddStartScene(true, OpenSceneMode.Single);
				AddMainScene(false, OpenSceneMode.Additive);
				AddAllLevelScenes(OpenSceneMode.Additive);
				_loadAllScene = false;
			}
		}

		private static void AddStartScene(bool setActive, OpenSceneMode sceneMode)
		{
			EditorSceneManager.OpenScene("Assets/Scenes/1_StartScene/StartScene.unity", sceneMode);
			if(setActive) SceneManager.SetActiveScene(SceneManager.GetSceneByName("StartScene"));
		}
		
		private static void AddMainScene(bool setActive, OpenSceneMode sceneMode)
		{
			EditorSceneManager.OpenScene("Assets/Scenes/2_MainScene/MainScene.unity", sceneMode);
			if(setActive) SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainScene"));
		}
		
		private static void AddAllLevelScenes(OpenSceneMode sceneMode)
		{
			var folders = AssetDatabase.GetSubFolders("Assets/Scenes/3_Levels");

			var levels = AssetDatabase.FindAssets("t:scene", folders);
			foreach (var level in levels)
			{
				var levelPath = AssetDatabase.GUIDToAssetPath(level);
				EditorSceneManager.OpenScene(levelPath, sceneMode);
				EditorSceneManager.CloseScene(SceneManager.GetSceneByPath(levelPath),false);
			}
		}

		private static void DisableMenuLevelLoading()
		{
			var loadingHandler = GameObject.Find("LoadingHandler").GetComponent<LoadingHandler>();

			loadingHandler.loadMenu = false;
			loadingHandler.activeLevelIndex = 2;
		}

		private static void CreateInitializerGameObject()
		{
			var init = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Systems/Initializer.prefab", typeof(Initializer));

			var initInstance = PrefabUtility.InstantiatePrefab(init);
			initInstance.name = "AutoCreatedInitializer (Destroy After PlayMode)";

			var instance = GameObject.Find("AutoCreatedInitializer (Destroy After PlayMode)");
			instance.tag = "DestroyOnEditMode";
		}
	}
}