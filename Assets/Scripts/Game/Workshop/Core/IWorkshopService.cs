using Common.Level.Core;
using Game.Common.Level.Data;

namespace Game.Workshop.Core
{
    public interface IWorkshopService : ILevelLoader
    {
        void ResetLevel();
        void LoadCurrentLevel();
        void SaveLevel();
        LevelData GetLevelData();
    }
}