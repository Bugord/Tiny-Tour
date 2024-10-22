using Core.Navigation;
using Game.Project.GameState.Systems;
using UI.Screens;

namespace Game.Main.GameState.States
{
    public class MainMenuState : BaseGameState
    {
        private readonly INavigationService navigationService;
        
        private MainMenuScreen mainMenuScreen;
        
        public MainMenuState(GameStateMachine gameStateMachine, INavigationService navigationService) : base(gameStateMachine)
        {
            this.navigationService = navigationService;
        }
        
        public override void OnEnter()
        { 
            mainMenuScreen = navigationService.PushScreen<MainMenuScreen>();
            mainMenuScreen.PlayLevelsPressed += OnPlayLevelsPressed;
            mainMenuScreen.EditLevelsPressed += OnEditLevelsPressed;
        }

        public override void OnExit()
        {
            mainMenuScreen.PlayLevelsPressed -= OnPlayLevelsPressed;
            mainMenuScreen.EditLevelsPressed -= OnEditLevelsPressed;
            navigationService.PopScreen(mainMenuScreen);
        }

        private void OnPlayLevelsPressed()
        {
            GameStateMachine.ChangeState<SelectLevelToPlayState>();
        }

        private void OnEditLevelsPressed()
        {
            GameStateMachine.ChangeState<SelectLevelToEditState>();
        }
    }
}