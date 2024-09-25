using Core.Navigation;
using States;
using UI;
using UI.Screens;

namespace Core.GameState.States
{
    public class MainMenuState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly GameStateMachine gameStateSystem;
        
        private MainMenuScreen mainMenuScreen;
        
        public MainMenuState(GameStateMachine gameStateSystem, INavigationService navigationService)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationService = navigationService;
        }
        
        public override void OnEnter()
        { 
            mainMenuScreen = navigationService.Push<MainMenuScreen>();
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
            gameStateSystem.ChangeState<SelectLevelToPlayState>();
        }

        private void OnEditLevelsPressed()
        {
            gameStateSystem.ChangeState<SelectLevelToEditState>();
        }
    }
}