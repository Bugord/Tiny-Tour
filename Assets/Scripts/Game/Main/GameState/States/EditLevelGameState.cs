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
    public class EditLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly ISessionManger sessionManger;
        private EditLevelScreen editLevelScreen;

        public EditLevelState(GameStateMachine gameStateMachine, INavigationService navigationService, ISessionManger sessionManger)
            : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.sessionManger = sessionManger;
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
            
            sessionManger.EndSession();
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