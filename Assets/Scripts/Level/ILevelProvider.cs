using System.Collections.Generic;

namespace Level
{
    public interface ILevelProvider
    {
        public void LoadAllLevels();
        public List<LevelData> GetAllLevels();
        public LevelData GetLevelByName(string name);
        public void SaveLevel(LevelData levelData);
    }
}