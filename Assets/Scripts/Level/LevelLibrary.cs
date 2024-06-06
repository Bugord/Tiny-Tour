using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Level.Data;
using UnityEngine;

namespace Level
{
    [CreateAssetMenu]
    public class LevelLibrary : ScriptableObject, ILevelProvider
    {
        private LevelData[] levelsData;

        private const string DirectoryPath = "./levels";

        public void LoadAllLevels()
        {
            var levels = new List<LevelData>();

            Directory.CreateDirectory(DirectoryPath);
            var files = Directory.GetFiles(DirectoryPath, "*.json");
            
            foreach (var file in files) {
                try {
                    var json = File.ReadAllText(file);
                    var level = JsonUtility.FromJson<LevelData>(json);
                    if (level != null) {
                        levels.Add(level);
                        Debug.Log($"Loaded {level.levelName}");
                    }
                }
                catch (Exception ex) {
                    Debug.LogException(ex);
                }
            }

            levelsData = levels.ToArray();
        }

        public LevelData[] GetCachedLevels()
        {
            return levelsData;
        }

        public LevelData GetLevelByName(string name)
        {
            return levelsData.FirstOrDefault(level => level.levelName == name);
        }

        public void SaveLevel(LevelData levelData)
        {
            Directory.CreateDirectory(DirectoryPath);
            var json = JsonUtility.ToJson(levelData, true);
            File.WriteAllText(Path.Combine(DirectoryPath, levelData.levelName + ".json"), json);
        }

        public LevelData GetLevelByIndex(int index)
        {
            return levelsData[index];
        }
    }
}