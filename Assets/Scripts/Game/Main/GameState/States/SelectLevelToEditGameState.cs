using Core.Navigation;
using Game.Common.Level.Core;
using Game.Main.Session.Core;
using Game.Main.UI.Screens;
using Game.Main.Workshop;
using Game.Project.GameState.Systems;
using Level;
using UnityEngine;

namespace Game.Main.GameState.States
{
    public class SelectLevelToEditState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly LevelManager levelManager;
        private readonly IWorkshopService workshopService;

        private EditLevelSelectScreen editLevelSelectSelectScreen;
        
        public SelectLevelToEditState(GameStateMachine gameStateMachine, INavigationService navigationService,
            LevelManager levelManager, IWorkshopService workshopService) : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.levelManager = levelManager;
            this.workshopService = workshopService;
        }

        public override void OnEnter()
        {
            editLevelSelectSelectScreen = navigationService.PushScreen<EditLevelSelectScreen>();
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
            var levelData = levelManager.GetLevelByIndex(levelIndex);
            workshopService.SetLevelData(levelData);
            GameStateMachine.ChangeState<EditLevelState>();
        }

        private void OnBackPressed()
        {
            GameStateMachine.ChangeState<MainMenuState>();
        }

        private void OnCreateNewPressed()
        {
            var newLevelData = levelManager.CreateNewLevel("New Level " + Random.Range(0, 1000));
            workshopService.SetLevelData(newLevelData);
            
            GameStateMachine.ChangeState<EditLevelState>();
        }
    }
}