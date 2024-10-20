using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Data;

namespace Level
{
    public interface ILevelProvider
    {
        public LevelData[] GetLevels();
        public LevelData GetLevelByName(string name);
        public LevelData GetLevelByIndex(int index);
        public LevelData CreateNewLevel(string levelName);
        void UpdateLevel(LevelData levelData);
        void SaveNewLevel(LevelData levelData);
        void SaveLevel(LevelData levelData);
        void LoadLevels();
    }
}