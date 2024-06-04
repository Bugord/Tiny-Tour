using Level.Data;

namespace Level
{
    public interface ILevelLoader
    {
        public void LoadLevel(LevelData levelData);
    }
}