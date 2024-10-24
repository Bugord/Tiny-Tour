using Game.Common.Level.Data;

namespace Game.Common.Level.Core
{
    public interface ILevelLoader
    {
        void LoadLevel(LevelData levelData);
    }
}