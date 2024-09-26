using Level.Data;

namespace Gameplay
{
    public interface ILevelLoader
    {
        void LoadLevel(LevelData levelData);
    }
}