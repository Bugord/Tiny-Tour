using Core.Navigation;
using Game.Main.Session.Core;
using Game.Project.GameState.Systems;
using Level;
using UI.Screens;

namespace Game.Main.GameState.States
{
    public class SelectLevelToPlayState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly LevelManager levelManager;
        private readonly ISessionManger sessionManger;

        private PlayLevelSelectScreen playLevelSelectSelectScreen;

        public SelectLevelToPlayState(GameStateMachine gameStateMachine, INavigationService navigationService,
            LevelManager levelManager, ISessionManger sessionManger) : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.levelManager = levelManager;
            this.sessionManger = sessionManger;
        }

        public override void OnEnter()
        {
            playLevelSelectSelectScreen = navigationService.PushScreen<PlayLevelSelectScreen>();
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
            var levelData = levelManager.GetLevelByIndex(levelIndex);
            sessionManger.StartSession(levelData);
            
            GameStateMachine.ChangeState<PlayLevelState>();
        }

        private void OnBackPressed()
        {
            GameStateMachine.ChangeState<MainMenuState>();
        }
    }
}