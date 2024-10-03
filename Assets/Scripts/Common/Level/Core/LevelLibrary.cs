using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Cysharp.Threading.Tasks;
using Level.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Level
{
    [CreateAssetMenu]
    public class LevelLibrary : ScriptableObject, ILevelProvider
    {
        [SerializeField]
        private List<LevelData> levelsData;

        private const string ResouceDirectoryPath = "./Assets/Resource/Levels";

        [ContextMenu("Load from resources")]
        public void LoadAllLevels()
        {
            var levels = new List<LevelData>();

            var levelFiles = Resources.LoadAll<TextAsset>("Levels");

            foreach (var file in levelFiles) {
                try {
                    var json = file.text;
                    var level = JsonUtility.FromJson<LevelData>(json);
                    if (level != null) {
                        levels.Add(level);
                        Debug.Log($"Loaded {level.logisticData.roadTileData.Length}");
                    }
                }
                catch (Exception ex) {
                    Debug.LogException(ex);
                }
            }

            levelsData = levels;
        }

        [ContextMenu("Save to Resources")]
        public void SaveToResources()
        {
            Directory.CreateDirectory(ResouceDirectoryPath);
            foreach (var levelData in levelsData) {
                var json = JsonUtility.ToJson(levelData, true);
                File.WriteAllText(Path.Combine(ResouceDirectoryPath, levelData.levelName + ".json"), json);
            }
        }

        public LevelData[] GetLevels()
        {
            return levelsData.Select(data => data.Copy()).ToArray();
        }

        public LevelData GetLevelByName(string name)
        {
            return levelsData.FirstOrDefault(level => level.levelName == name);
        }

        public void UpdateLevel(LevelData levelData)
        {
            var levelToUpdate = levelsData.FirstOrDefault(data => data.levelName == levelData.levelName);
            if (levelToUpdate == null) {
                Debug.LogWarning($"Level with name {levelData.levelName} is not added");
                return;
            }

            levelToUpdate.SetData(levelData);
        }

        public void SaveNewLevel(LevelData levelData)
        {
            var existingLevel = levelsData.FirstOrDefault(data => data.levelName == levelData.levelName);
            if (existingLevel != null) {
                Debug.LogWarning($"Level with name {levelData.levelName} already exists");
                return;
            }

            levelsData.Add(levelData);
        }
        
        public void SaveLevel(LevelData levelData)
        {
            var existingLevel = levelsData.FirstOrDefault(data => data.levelName == levelData.levelName);
            if (existingLevel != null) {
                UpdateLevel(levelData);
            }
            else {
                SaveNewLevel(levelData);
            }
        }

        public LevelData CreateNewLevel(string levelName)
        {
            return new LevelData(levelName);
        }

        public LevelData GetLevelByIndex(int index)
        {
            return levelsData[index];
        }
    }
}