using UI;
using UI.Screens;

namespace States
{
    public class MainMenuState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        
        private MainMenuScreen mainMenuScreen;
        
        public MainMenuState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
        }
        
        public override void OnEnter()
        { 
            mainMenuScreen = navigationSystem.Push<MainMenuScreen>();
            mainMenuScreen.PlayLevelsPressed += OnPlayLevelsPressed;
            mainMenuScreen.EditLevelsPressed += OnEditLevelsPressed;
        }

        public override void OnExit()
        {
            mainMenuScreen.PlayLevelsPressed -= OnPlayLevelsPressed;
            mainMenuScreen.EditLevelsPressed -= OnEditLevelsPressed;
            mainMenuScreen.Close();
        }

        private void OnPlayLevelsPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.SelectLevelToPlayState);
        }

        private void OnEditLevelsPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.SelectLevelToEditState);
        }
    }
}