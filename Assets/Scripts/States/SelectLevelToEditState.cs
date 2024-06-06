using Level;
using UI;
using UI.Screens;
using UnityEngine;

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
            editLevelSelectSelectScreen.CreateNewPressed += OnCreateNewPressed;
            
            levelManager.LoadLevels();
            var levels = levelManager.GetLevels();
            
            editLevelSelectSelectScreen.SetLevels(levels);
        }

        public override void OnExit()
        {
            editLevelSelectSelectScreen.BackPressed -= OnBackPressed;
            editLevelSelectSelectScreen.LevelSelected -= OnLevelSelected;
            editLevelSelectSelectScreen.CreateNewPressed -= OnCreateNewPressed;
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

        private void OnCreateNewPressed()
        {
            var newLevel = levelManager.CreateNewLevel("New Level " + Random.Range(0, 1000));
            levelManager.SelectLevel(newLevel);
            
            gameStateSystem.ChangeState(gameStateSystem.EditLevelState);
        }
    }
}