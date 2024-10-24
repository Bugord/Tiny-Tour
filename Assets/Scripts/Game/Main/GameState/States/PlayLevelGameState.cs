using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Core;
using Game.Gameplay.Core;
using Game.Gameplay.PlayState.Core;
using Game.Gameplay.PlayState.States;
using Game.Main.Session.Core;
using Game.Main.UI.Popups;
using Game.Main.UI.Screens;
using Game.Project.GameState.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Main.GameState.States
{
    public class PlayLevelGameState : BaseGameState
    {
        private readonly SceneContextRegistry sceneContextRegistry;
        private readonly ISessionManger sessionManger;
        private readonly INavigationService navigationService;
        private readonly LevelManager levelManager;

        private IPlayService playService;

        public PlayLevelGameState(GameStateMachine gameStateMachine, SceneContextRegistry sceneContextRegistry,
            ISessionManger sessionManger, INavigationService navigationService, LevelManager levelManager) : base(gameStateMachine)
        {
            this.sceneContextRegistry = sceneContextRegistry;
            this.sessionManger = sessionManger;
            this.navigationService = navigationService;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            playService.PlayingExitCalled -= ReturnToMainMenu;
            SceneManager.UnloadSceneAsync(SceneNames.PlaySceneName);
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(SceneNames.PlaySceneName, LoadSceneMode.Additive);
            await UniTask.WaitForEndOfFrame();

            var playSceneContainer = sceneContextRegistry.GetSceneContextForScene(SceneNames.PlaySceneName).Container;

            var levelData = sessionManger.CurrentSession.LevelData;
            playService = playSceneContainer.Resolve<IPlayService>();
            playService.PlayLevel(levelData);

            playService.PlayingExitCalled += ReturnToMainMenu;
            playService.LevelPassed += OnPlayingPassed;
        }

        private void OnPlayingPassed()
        {
            ShowLevelPassedPopup().Forget();
        }

        private async UniTask ShowLevelPassedPopup()
        {
            var levelPassedPopup = navigationService.PushPopup<LevelPassedPopup>();
            await levelPassedPopup.Task;

            navigationService.ClosePopup(levelPassedPopup);
            playService.ClearLevel();
            
            var finishedLevelData = sessionManger.CurrentSession.LevelData;
            sessionManger.EndSession();

            var nextLevelData = levelManager.GetNextLevel(finishedLevelData);
            if (nextLevelData == null) {
                playService.RestartLevel();
                return;
            }

            sessionManger.StartSession(nextLevelData);
            playService.PlayLevel(nextLevelData);
        }

        private void ReturnToMainMenu()
        {
            GameStateMachine.ChangeState<SelectLevelToPlayState>();
        }
    }
}