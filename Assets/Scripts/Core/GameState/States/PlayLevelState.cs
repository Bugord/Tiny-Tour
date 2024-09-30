using System.Collections;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Level;
using States;
using UI;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameState.States
{
    public class PlayLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private const string PlayLevelScene = "PlayLevelScene";

        private PlayLevelScreen playLevelScreen;

        public PlayLevelState(GameStateMachine gameStateMachine, INavigationService navigationService)
            : base(gameStateMachine)
        {
            this.navigationService = navigationService;
        }

        public override void OnEnter()
        {
            playLevelScreen = navigationService.Push<PlayLevelScreen>();
            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            navigationService.PopScreen(playLevelScreen);
            SceneManager.UnloadSceneAsync(PlayLevelScene);
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(PlayLevelScene, LoadSceneMode.Additive);
        }
    }
}