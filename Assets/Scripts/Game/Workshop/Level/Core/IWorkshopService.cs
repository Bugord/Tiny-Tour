using Common.Level.Core;

namespace LevelEditor.Level.Core
{
    public interface IWorkshopService : ILevelLoader
    {
        void ResetLevel();
        void LoadCurrentLevel();
        void SaveLevel();
    }
}