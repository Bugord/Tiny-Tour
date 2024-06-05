using Level;
using UI;
using UI.Screens;

namespace States
{
    public class SelectLevelToEditState : BaseGameState
    {
        private readonly NavigationSystem navigationSystem;
        private readonly GameStateSystem gameStateSystem;
        private readonly LevelManager levelManager;

        private EditLevelSelectScreen editLevelSelectSelectScreen;
        
        public SelectLevelToEditState(GameStateSystem gameStateSystem, NavigationSystem navigationSystem, LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationSystem = navigationSystem;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            editLevelSelectSelectScreen = navigationSystem.Push<EditLevelSelectScreen>();
            editLevelSelectSelectScreen.BackPressed += OnBackPressed;
            editLevelSelectSelectScreen.LevelSelected += OnLevelSelected;
            
            levelManager.LoadLevels();
            var levels = levelManager.GetLevels();
            
            editLevelSelectSelectScreen.SetLevels(levels);
        }

        public override void OnExit()
        {
            editLevelSelectSelectScreen.BackPressed -= OnBackPressed;
            editLevelSelectSelectScreen.LevelSelected -= OnLevelSelected;
            editLevelSelectSelectScreen.Close();
        }

        private void OnLevelSelected(int levelIndex)
        {
            levelManager.SelectLevel(levelIndex);
            gameStateSystem.ChangeState(gameStateSystem.EditLevelState);
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState(gameStateSystem.MainMenuState);
        }
    }
}