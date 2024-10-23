using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Gameplay.Core;
using Game.Main.Session.Core;
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

        private IPlayService playService;

        public PlayLevelGameState(GameStateMachine gameStateMachine, SceneContextRegistry sceneContextRegistry,
            ISessionManger sessionManger) : base(gameStateMachine)
        {
            this.sceneContextRegistry = sceneContextRegistry;
            this.sessionManger = sessionManger;
        }

        public override void OnEnter()
        {
            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            playService.PlayingEnded -= ReturnToMainMenu;
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

            playService.PlayingEnded += ReturnToMainMenu;
        }

        private void ReturnToMainMenu()
        {
            GameStateMachine.ChangeState<SelectLevelToPlayState>();
        }
    }
}