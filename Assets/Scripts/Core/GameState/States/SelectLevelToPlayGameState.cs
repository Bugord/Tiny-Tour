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
        private readonly LevelManager levelManager;

        private PlayLevelSelectScreen playLevelSelectSelectScreen;

        public SelectLevelToPlayState(GameStateMachine gameStateMachine, INavigationService navigationService,
            LevelManager levelManager) : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            playLevelSelectSelectScreen = navigationService.Push<PlayLevelSelectScreen>();
            playLevelSelectSelectScreen.BackPressed += OnBackPressed;
            playLevelSelectSelectScreen.LevelSelected += OnLevelSelected;

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
            GameStateMachine.ChangeState<PlayLevelState>();
        }

        private void OnBackPressed()
        {
            GameStateMachine.ChangeState<MainMenuState>();
        }
    }
}