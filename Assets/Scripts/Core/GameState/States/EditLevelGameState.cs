using Core.Navigation;
using Cysharp.Threading.Tasks;
using States;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.GameState.States
{
    public class EditLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private EditLevelScreen editLevelScreen;

        public EditLevelState(GameStateMachine gameStateMachine, INavigationService navigationService)
            : base(gameStateMachine)
        {
            this.navigationService = navigationService;
        }

        public override void OnEnter()
        {
            editLevelScreen = navigationService.PushScreen<EditLevelScreen>();
            editLevelScreen.BackPressed += ReturnToMainMenu;
            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            editLevelScreen.BackPressed -= ReturnToMainMenu;
            navigationService.PopScreen(editLevelScreen);
            SceneManager.UnloadSceneAsync(SceneNames.EditorSceneName);
        }

        private void ReturnToMainMenu()
        {
            GameStateMachine.ChangeState<SelectLevelToEditState>();
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(SceneNames.EditorSceneName, LoadSceneMode.Additive);
        }
    }
}