using Game.Common.Level.Data;

namespace Common.Level.Core
{
    public interface ILevelLoader
    {
        void LoadLevel(LevelData levelData);
    }
}