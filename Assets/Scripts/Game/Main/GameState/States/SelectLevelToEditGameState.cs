using Core.Navigation;
using Game.Main.Session.Core;
using Game.Project.GameState.Systems;
using Level;
using UI.Screens;
using UnityEngine;

namespace Game.Main.GameState.States
{
    public class SelectLevelToEditState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly LevelManager levelManager;
        private readonly ISessionManger sessionManger;

        private EditLevelSelectScreen editLevelSelectSelectScreen;
        
        public SelectLevelToEditState(GameStateMachine gameStateMachine, INavigationService navigationService,
            LevelManager levelManager, ISessionManger sessionManger) : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.levelManager = levelManager;
            this.sessionManger = sessionManger;
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
            sessionManger.StartSession(levelData);
            GameStateMachine.ChangeState<EditLevelState>();
        }

        private void OnBackPressed()
        {
            GameStateMachine.ChangeState<MainMenuState>();
        }

        private void OnCreateNewPressed()
        {
            var newLevelData = levelManager.CreateNewLevel("New Level " + Random.Range(0, 1000));
            sessionManger.StartSession(newLevelData);
            
            GameStateMachine.ChangeState<EditLevelState>();
        }
    }
}