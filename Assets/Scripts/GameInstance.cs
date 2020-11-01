using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;

public class GameInstance : MonoBehaviour
{
    private static GameInstance instance;
    public static GameInstance Instance => instance;
    public List<SceneData> SceneDataList;
    public string MenuSceneName;
    private string CurrentScene;
    
#if UNITY_EDITOR
    [Header("Assets")]
    public SceneAsset PersistentSceneAssets;
    public SceneAsset MenuSceneAssets;
    public List<SceneAsset> SceneAssets;
    private void OnValidate()
    {
        while(SceneAssets.Count > SceneDataList.Count)
        {
            SceneDataList.Add(new SceneData());
        }

        for (int i = 0; i < SceneAssets.Count; i++)
        {
            if (SceneAssets[i])
            {
                SceneDataList[i].SceneName = SceneAssets[i].name;                
            }
        }

        if (MenuSceneAssets)
        {
            MenuSceneName = MenuSceneAssets.name;
        }
        
        
        var editorBuildSettingsScenes = new List<EditorBuildSettingsScene>();
            
        var persistentScenePath = AssetDatabase.GetAssetPath(PersistentSceneAssets);
        if(!string.IsNullOrEmpty(persistentScenePath)) 
            editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(persistentScenePath, true));
            
        var menuScenePath = AssetDatabase.GetAssetPath(MenuSceneAssets);
        if(!string.IsNullOrEmpty(menuScenePath)) 
            editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(menuScenePath, true));

        foreach (var sceneName in SceneAssets)
        {
            var gameScenePath = AssetDatabase.GetAssetPath(sceneName);
            if(!string.IsNullOrEmpty(gameScenePath))
                editorBuildSettingsScenes.Add(new EditorBuildSettingsScene(gameScenePath, true));
        }
        
        EditorBuildSettings.scenes = editorBuildSettingsScenes.ToArray();
    }
#endif

    private void Awake()
    {
        instance = this;
        SceneManager.sceneLoaded += OnSceneLoaded;
        LoadMenuScene();
    }
    
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"{scene.name} loaded");
    }

    public void PlayerStart()
    {
        
    }

    private void LoadMenuScene()
    {
        SceneManager.LoadScene(MenuSceneName, LoadSceneMode.Additive);
        CurrentScene = MenuSceneName;
    }

    private void LoadGameScene(string scene)
    {
        SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        CurrentScene = scene;
    }

    private void UnloadCurrentScene()
    {
        SceneManager.UnloadSceneAsync(CurrentScene);
    }
    public void LevelSelectedOnMenu(string sceneName)
    {
        Debug.Log($"{sceneName} selected");
        UnloadCurrentScene();
        LoadGameScene(sceneName);
    }
    public void OnSceneFinish(Scene scene)
    {
        Debug.Log($"GameInstance::OnSceneFinish({scene.name})");
        foreach(var sceneData in SceneDataList)
        {
            if (sceneData.SceneName == scene.name) sceneData.isCleared = true;
        }
        UnloadCurrentScene();
        LoadMenuScene();
    }
}

[Serializable]
public class SceneData
{
    public string SceneName;
    [Range(1, 5)]public int difficulty = 1;
    public bool isCleared = false;
}
