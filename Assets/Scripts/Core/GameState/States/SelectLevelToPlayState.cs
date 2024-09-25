using Core.GameState;
using Core.GameState.States;
using Core.Navigation;
using Level;
using UI;
using UI.Screens;

namespace States
{
    public class SelectLevelToPlayState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly GameStateMachine gameStateSystem;
        private readonly LevelManager levelManager;

        private PlayLevelSelectScreen playLevelSelectSelectScreen;

        public SelectLevelToPlayState(GameStateMachine gameStateSystem, INavigationService navigationService,
            LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationService = navigationService;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            playLevelSelectSelectScreen = navigationService.Push<PlayLevelSelectScreen>();
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
            navigationService.PopScreen(playLevelSelectSelectScreen);
        }
        
        private void OnLevelSelected(int levelIndex)
        {
            levelManager.SelectLevel(levelIndex);
            gameStateSystem.ChangeState<PlayLevelState>();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState<MainMenuState>();
        }
    }
}