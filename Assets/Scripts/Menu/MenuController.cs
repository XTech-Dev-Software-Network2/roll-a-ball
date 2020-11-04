using System;
using System.Collections.Generic;
using UnityEngine;

namespace Menu
{
    public class MenuController : MonoBehaviour
    {
        public LevelUIComponent PrefabLevelUITemplate;
        public List<LevelUIComponent> levelUIComponents = new List<LevelUIComponent>();
        public RectTransform ContentView;

        private void Awake()
        {
            foreach (var elem in levelUIComponents)
            {
                DestroyImmediate(elem.gameObject);
            }
            levelUIComponents.Clear();
            if (GameInstance.Instance)
            {
                var scenes = GameInstance.Instance.SceneDataList;
                bool playable = true;
                foreach (var scene in scenes)
                {
                    var go = Instantiate(PrefabLevelUITemplate, ContentView.gameObject.transform);
                    go.SetText(scene.SceneName, scene.difficulty, playable);
                    go.name = scene.SceneName;
                    levelUIComponents.Add(go);
                    playable = scene.isCleared; // prev scene isCleared
                }
            }
        }
    }
}
