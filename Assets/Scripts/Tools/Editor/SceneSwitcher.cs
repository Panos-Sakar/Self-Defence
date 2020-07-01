using SelfDef.Systems.Loading;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityToolbarExtender;

namespace SelfDef.Tools.Editor
{
	[InitializeOnLoad]
	public class SceneSwitchLeftButton
	{
		static SceneSwitchLeftButton()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarLeftGUI);
			ToolbarExtender.RightToolbarGUI.Add(OnToolbarRightGUI);
		}

		private static void OnToolbarLeftGUI()
		{
			GUILayout.FlexibleSpace();
			if(GUILayout.Button(new GUIContent(" PB ", "Load ProBuilder Scene"), "Button"))
			{
				SceneHelper.LoadProBuilderScene();
			}
			if(GUILayout.Button(new GUIContent("ALL", "Load All Levels"), "Button"))
			{
				SceneHelper.LoadAllLevels();
			}
		}

		private static void OnToolbarRightGUI()
		{

			if(GUILayout.Button(new GUIContent("Load Main & Play", "Load Main Scene And Play"),"Button"))
			{
				SceneHelper.LoadMainSceneAndPlay();
			}
			GUILayout.FlexibleSpace();
		}
	}

	internal static class SceneHelper
	{
		private static string _scenePathToOpen;
		private static bool _loadMainSceneAndPlay;
		private static bool _loadAllScene;
		private static bool _loadProBuilderScene;

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
		
		public static void LoadProBuilderScene()
		{
			if(EditorApplication.isPlaying)
			{
				EditorApplication.isPlaying = false;
			}

			_loadProBuilderScene = true;
			_scenePathToOpen = "Assets/Scenes/Helper Scenes/ProBuilder Objects/ProBuilder.unity";
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
				
				AddMainScene(true,false, OpenSceneMode.Additive);

				var startScene = SceneManager.GetSceneByName("StartScene");
				EditorSceneManager.CloseScene(startScene,false);
				DisableMenuLevelLoading();

				CreateInitializerGameObject();
				
				_loadMainSceneAndPlay = false;
				EditorApplication.isPlaying = true;
			}
			else if (_loadAllScene)
			{
				AddStartScene(true,false, OpenSceneMode.Single);
				AddMainScene(false,true, OpenSceneMode.Additive);
				AddAllLevelScenes(true,OpenSceneMode.Additive);
				_loadAllScene = false;
			}
			else if (_loadProBuilderScene)
			{
				AddSceneByPath(_scenePathToOpen, true, false, OpenSceneMode.Single);
				_loadProBuilderScene = false;
				_scenePathToOpen = null;
			}
		}

		private static void AddSceneByPath(string scenePath,bool setActive,bool isClosed, OpenSceneMode sceneMode)
		{
			EditorSceneManager.OpenScene(scenePath, sceneMode);
			if(setActive) SceneManager.SetActiveScene(SceneManager.GetSceneByPath(scenePath));
			if(isClosed) EditorSceneManager.CloseScene(SceneManager.GetSceneByPath(scenePath),false);
		}
		
		private static void AddStartScene(bool setActive,bool isClosed, OpenSceneMode sceneMode)
		{
			const string scenePath = "Assets/Scenes/1_StartScene/StartScene.unity";
			
			EditorSceneManager.OpenScene(scenePath, sceneMode);

			var scene = SceneManager.GetSceneByPath(scenePath);
			if(setActive) SceneManager.SetActiveScene(scene);
			if(isClosed) EditorSceneManager.CloseScene(scene,false);
		}
		
		private static void AddMainScene(bool setActive,bool isClosed, OpenSceneMode sceneMode)
		{
			const string scenePath = "Assets/Scenes/2_MainScene/MainScene.unity";
			
			EditorSceneManager.OpenScene(scenePath, sceneMode);
			
			var scene = SceneManager.GetSceneByPath(scenePath);
			if(setActive) SceneManager.SetActiveScene(scene);
			if(isClosed) EditorSceneManager.CloseScene(scene,false);
		}
		
		private static void AddAllLevelScenes(bool loadAssClosed,OpenSceneMode sceneMode)
		{
			var folders = AssetDatabase.GetSubFolders("Assets/Scenes/3_Levels");

			var levels = AssetDatabase.FindAssets("t:scene", folders);
			foreach (var level in levels)
			{
				var levelPath = AssetDatabase.GUIDToAssetPath(level);
				EditorSceneManager.OpenScene(levelPath, sceneMode);
				
				if (loadAssClosed)
				{
					var scene = SceneManager.GetSceneByPath(levelPath);
					EditorSceneManager.CloseScene(scene,false);
				}
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