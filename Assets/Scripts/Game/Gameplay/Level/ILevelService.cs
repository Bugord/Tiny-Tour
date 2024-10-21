using Common.Level.Core;

namespace Game.Gameplay.Level
{
    public interface ILevelService : ILevelLoader
    {
        void ResetLevel();
    }
}