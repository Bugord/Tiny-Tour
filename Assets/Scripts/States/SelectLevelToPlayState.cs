using Level;
using UI;
using UI.Screens;

namespace States
{
    public class SelectLevelToPlayState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        private readonly LevelManager levelManager;

        private PlayLevelSelectScreen playLevelSelectSelectScreen;

        public SelectLevelToPlayState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem,
            LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            playLevelSelectSelectScreen = navigationSystem.Push<PlayLevelSelectScreen>();
            playLevelSelectSelectScreen.BackPressed += OnBackPressed;
            playLevelSelectSelectScreen.LevelSelected += OnLevelSelected;

            levelManager.LoadLevels();
            var levels = levelManager.GetLevels();

            playLevelSelectSelectScreen.SetLevels(levels);
        }

        public override void OnExit()
        {
            playLevelSelectSelectScreen.BackPressed -= OnBackPressed;
            playLevelSelectSelectScreen.LevelSelected -= OnLevelSelected;
            playLevelSelectSelectScreen.Close();
        }
        
        private void OnLevelSelected(int levelIndex)
        {
            levelManager.SelectLevel(levelIndex);
            gameStateSystem.ChangeState(gameStateSystem.PlayLevelState);
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.MainMenuState);
        }
    }
}