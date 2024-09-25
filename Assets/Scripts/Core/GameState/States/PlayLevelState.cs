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
        private readonly GameStateMachine gameStateSystem;
        private readonly LevelManager levelManager;

        private GameSession gameSession;
        private PlayLevelScreen playLevelScreen;
        
        private const string GameSessionTag = "GameSession";
        private const string PlayLevelScene = "PlayLevelScene";

        public PlayLevelState(GameStateMachine gameStateSystem, INavigationService navigationService,
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
            playLevelScreen.ResetPressed += OnResetPressed;
            playLevelScreen.PreviousLevelPressed += OnPreviousLevelPressed;
            playLevelScreen.NextLevelPressed += OnNextLevelPressed;

            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            SceneManager.UnloadSceneAsync(PlayLevelScene);

            playLevelScreen.BackPressed -= OnBackPressed;
            playLevelScreen.PlayPressed -= OnPlayPressed;
            playLevelScreen.ResetPressed -= OnResetPressed;
            playLevelScreen.PreviousLevelPressed -= OnPreviousLevelPressed;
            playLevelScreen.NextLevelPressed -= OnNextLevelPressed;
            navigationService.PopScreen(playLevelScreen);
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
            gameSession.SetupEditor(playLevelScreen.TilemapEditorUI);
            gameSession.LoadLevel(selectedLevel);
        }

        private void OnPlayPressed()
        {
            gameSession.Play();
        }

        private void OnBackPressed()
        {
            gameStateSystem.ChangeState<SelectLevelToPlayState>();
        }

        private void OnResetPressed()
        {
            gameSession.ResetCars();
        }

        private void OnPreviousLevelPressed()
        {
            var previousLevel = levelManager.GetPreviousLevel();
            if (previousLevel == null) {
                return;
            }
            levelManager.SelectLevel(previousLevel);
            gameSession.CloseLevel();
            gameSession.LoadLevel(previousLevel);
        }

        private void OnNextLevelPressed()
        {
            var nextLevel = levelManager.GetNextLevel();
            if (nextLevel == null) {
                return;
            }
            levelManager.SelectLevel(nextLevel);
            gameSession.CloseLevel();
            gameSession.LoadLevel(nextLevel);
        }
    }
}