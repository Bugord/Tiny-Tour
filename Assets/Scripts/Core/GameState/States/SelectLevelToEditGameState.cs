using Core.Navigation;
using Level;
using States;
using UI;
using UI.Screens;
using UnityEngine;

namespace Core.GameState.States
{
    public class SelectLevelToEditState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly LevelManager levelManager;

        private EditLevelSelectScreen editLevelSelectSelectScreen;
        
        public SelectLevelToEditState(GameStateMachine gameStateMachine, INavigationService navigationService, LevelManager levelManager) : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            editLevelSelectSelectScreen = navigationService.Push<EditLevelSelectScreen>();
            editLevelSelectSelectScreen.BackPressed += OnBackPressed;
            editLevelSelectSelectScreen.LevelSelected += OnLevelSelected;
            editLevelSelectSelectScreen.CreateNewPressed += OnCreateNewPressed;
            
            var levels = levelManager.GetLevels();
            editLevelSelectSelectScreen.SetLevels(levels);
        }

        public override void OnExit()
        {
            editLevelSelectSelectScreen.BackPressed -= OnBackPressed;
            editLevelSelectSelectScreen.LevelSelected -= OnLevelSelected;
            editLevelSelectSelectScreen.CreateNewPressed -= OnCreateNewPressed;
            navigationService.PopScreen(editLevelSelectSelectScreen);
        }

        private void OnLevelSelected(int levelIndex)
        {
            levelManager.SelectLevel(levelIndex);
            GameStateMachine.ChangeState<EditLevelState>();
        }

        private void OnBackPressed()
        {
            GameStateMachine.ChangeState<MainMenuState>();
        }

        private void OnCreateNewPressed()
        {
            var newLevel = levelManager.CreateNewLevel("New Level " + Random.Range(0, 1000));
            levelManager.SelectLevel(newLevel);
            
            GameStateMachine.ChangeState<EditLevelState>();
        }
    }
}