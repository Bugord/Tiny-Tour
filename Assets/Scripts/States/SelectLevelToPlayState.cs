using Level;
using UI;
using UI.Screens;

namespace States
{
    public class SelectLevelToPlayState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        private readonly ILevelProvider levelProvider;

        private PlayLevelScreen playLevelSelectScreen;

        public SelectLevelToPlayState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem,
            ILevelProvider levelProvider)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
            this.levelProvider = levelProvider;
        }

        public override void OnEnter()
        {
            playLevelSelectScreen = navigationSystem.Push<PlayLevelScreen>();
            playLevelSelectScreen.BackPressed += OnBackPressed;

            levelProvider.LoadAllLevels();
            var levels = levelProvider.GetCachedLevels();

            playLevelSelectScreen.SetLevels(levels);
        }

        public override void OnExit()
        {
            playLevelSelectScreen.BackPressed -= OnBackPressed;
            playLevelSelectScreen.Close();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.MainMenuState);
        }
    }
}