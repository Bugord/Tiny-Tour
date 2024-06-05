using Level;
using UI;
using UI.Screens;

namespace States
{
    public class SelectLevelToEditState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        private readonly ILevelProvider levelProvider;

        private EditLevelScreen editLevelSelectScreen;
        
        public SelectLevelToEditState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem, ILevelProvider levelProvider)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
            this.levelProvider = levelProvider;
        }

        public override void OnEnter()
        {
            editLevelSelectScreen = navigationSystem.Push<EditLevelScreen>();
            editLevelSelectScreen.BackPressed += OnBackPressed;
            
            levelProvider.LoadAllLevels();
            var levels = levelProvider.GetCachedLevels();
            
            editLevelSelectScreen.SetLevels(levels);
        }

        public override void OnExit()
        {
            editLevelSelectScreen.BackPressed -= OnBackPressed;
            editLevelSelectScreen.Close();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.MainMenuState);
        }
    }
}