using Common.Level.Core;

namespace LevelEditor.Level.Core
{
    public interface ILevelEditorService : ILevelLoader
    {
        void ResetLevel();
    }
}