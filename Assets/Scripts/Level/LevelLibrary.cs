using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common;
using Cysharp.Threading.Tasks;
using Level.Data;
using UnityEngine;
using UnityEngine.Networking;

namespace Level
{
    [CreateAssetMenu]
    public class LevelLibrary : ScriptableObject, ILevelProvider
    {
        private LevelData[] levelsData;

        private static string DirectoryPath => "./Assets/Resources/Levels";

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

        public LevelData CreateNewLevel(string levelName)
        {
            return new LevelData {
                levelName = levelName,
                obstaclesData = Array.Empty<ObstacleTileData>(),
                terrainTilesData = Array.Empty<TerrainTileData>(),
                logisticData = new LogisticData {
                    roadTileData = Array.Empty<RoadTileData>(),
                    goalsData = Array.Empty<GoalData>(),
                    intermediatePointsData = Array.Empty<IntermediatePointData>()
                }
            };
        }

        public LevelData GetLevelByIndex(int index)
        {
            return levelsData[index];
        }
    }
}