using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Project.GameState.Systems;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Project.GameState.States
{
    public class PlayLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;

        private PlayLevelScreen playLevelScreen;

        public PlayLevelState(GameStateMachine gameStateMachine, INavigationService navigationService)
            : base(gameStateMachine)
        {
            this.navigationService = navigationService;
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