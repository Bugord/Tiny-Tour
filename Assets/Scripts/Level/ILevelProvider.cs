using System.Collections.Generic;

namespace Level
{
    public interface ILevelProvider
    {
        public void LoadAllLevels();
        public List<LevelData> GetCachedLevels();
        public LevelData GetLevelByName(string name);
        public LevelData GetLevelByIndex(int index);
        public void SaveLevel(LevelData levelData);
    }
}