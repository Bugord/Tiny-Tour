using Common;
using Level;
using Zenject;

namespace Gameplay
{
    public class PlayLevelService : IInitializable
    {
        private readonly LevelManager levelManager;
        private readonly ILevelLoader levelLoader;
        private readonly InGameEditor inGameEditor;

        public PlayLevelService(LevelManager levelManager, ILevelLoader levelLoader, InGameEditor inGameEditor)
        {
            this.levelManager = levelManager;
            this.levelLoader = levelLoader;
            this.inGameEditor = inGameEditor;
        }

        public void Initialize()
        {
            var level = levelManager.GetSelectedLevel();
            levelLoader.LoadLevel(level);
            
            inGameEditor.Enable();
        }
    }
}