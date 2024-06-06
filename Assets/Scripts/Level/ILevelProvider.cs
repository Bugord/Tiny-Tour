using System.Collections.Generic;
using Level.Data;

namespace Level
{
    public interface ILevelProvider
    {
        public void LoadAllLevels();
        public LevelData[] GetCachedLevels();
        public LevelData GetLevelByName(string name);
        public LevelData GetLevelByIndex(int index);
        public void SaveLevel(LevelData levelData);
        public LevelData CreateNewLevel(string levelName);
    }
}