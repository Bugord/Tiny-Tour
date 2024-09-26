using Level;
using Zenject;

namespace Gameplay
{
    public class PlayLevelService : IInitializable
    {
        private readonly LevelManager levelManager;
        private readonly ILevelLoader levelLoader;

        public PlayLevelService(LevelManager levelManager, ILevelLoader levelLoader)
        {
            this.levelManager = levelManager;
            this.levelLoader = levelLoader;
        }

        public void Initialize()
        {
            var level = levelManager.GetSelectedLevel();
            levelLoader.LoadLevel(level);
        }
    }
}