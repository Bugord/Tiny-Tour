using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Main.Session.Core;
using Game.Project.GameState.Systems;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Project.GameState.States
{
    public class PlayLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly ISessionManger sessionManger;

        private PlayLevelScreen playLevelScreen;

        public PlayLevelState(GameStateMachine gameStateMachine, INavigationService navigationService, ISessionManger sessionManger)
            : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.sessionManger = sessionManger;
        }

        public override void OnEnter()
        {
            playLevelScreen = navigationService.PushScreen<PlayLevelScreen>();
            playLevelScreen.BackPressed += ReturnToMainMenu;
            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            playLevelScreen.BackPressed -= ReturnToMainMenu;
            
            sessionManger.EndSession();
            navigationService.PopScreen(playLevelScreen);
            SceneManager.UnloadSceneAsync(SceneNames.PlaySceneName);
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(SceneNames.PlaySceneName, LoadSceneMode.Additive);
        }

        private void ReturnToMainMenu()
        {
            GameStateMachine.ChangeState<SelectLevelToPlayState>();
        }
    }
}