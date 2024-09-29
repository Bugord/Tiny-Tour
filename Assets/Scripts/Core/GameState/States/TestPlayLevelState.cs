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
    public class TestPlayLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly GameStateMachine gameStateSystem;
        private readonly LevelManager levelManager;

        private GameSession gameSession;
        private PlayLevelScreen playLevelScreen;
        
        private const string GameSessionTag = "GameSession";
        private const string PlayLevelScene = "PlayLevelScene";

        public TestPlayLevelState(GameStateMachine gameStateSystem, INavigationService navigationService,
            LevelManager levelManager)
        {
            this.gameStateSystem = gameStateSystem;
            this.navigationService = navigationService;
            this.levelManager = levelManager;
        }

        public override void OnEnter()
        {
            playLevelScreen = navigationService.Push<PlayLevelScreen>();
            playLevelScreen.BackPressed += OnBackPressed;
            playLevelScreen.PlayPressed += OnPlayPressed;

            LoadEditor().Forget();
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(PlayLevelScene, LoadSceneMode.Additive);
            gameSession = GameObject.FindGameObjectWithTag(GameSessionTag)
                ?.GetComponent<GameSession>();

            if (gameSession == null) {
                SceneManager.UnloadSceneAsync(PlayLevelScene);
                Debug.LogError($"Could not find {nameof(GameSession)}");
                gameStateSystem.ChangeState<SelectLevelToPlayState>();
            }
            
            var selectedLevel = levelManager.GetSelectedLevel();
            gameSession.LoadLevel(selectedLevel);
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(PlayLevelScene);

            playLevelScreen.BackPressed -= OnBackPressed;
            playLevelScreen.PlayPressed -= OnPlayPressed;
            navigationService.PopScreen(playLevelScreen);
        }

        private void OnPlayPressed()
        {
            gameSession.Play();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState<EditLevelState>();
        }
    }
}