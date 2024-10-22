using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Main.Session.Core;
using Game.Project.GameState.Systems;
using Game.Workshop.Core;
using UI.Screens;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Main.GameState.States
{
    public class EditLevelState : BaseGameState
    {
        private readonly INavigationService navigationService;
        private readonly ISessionManger sessionManger;
        private readonly SceneContextRegistry sceneContextRegistry;
        private EditLevelScreen editLevelScreen;

        public EditLevelState(GameStateMachine gameStateMachine, INavigationService navigationService,
            ISessionManger sessionManger, SceneContextRegistry sceneContextRegistry)
            : base(gameStateMachine)
        {
            this.navigationService = navigationService;
            this.sessionManger = sessionManger;
            this.sceneContextRegistry = sceneContextRegistry;
        }

        public override void OnEnter()
        {
            editLevelScreen = navigationService.PushScreen<EditLevelScreen>();
            editLevelScreen.BackPressed += ReturnToMainMenu;
            editLevelScreen.PlayPressed += OnPlayPressed;
            LoadEditor().Forget();
        }

        public override void OnExit()
        {
            editLevelScreen.BackPressed -= ReturnToMainMenu;
            editLevelScreen.PlayPressed -= OnPlayPressed;

            navigationService.PopScreen(editLevelScreen);
            SceneManager.UnloadSceneAsync(SceneNames.EditorSceneName);
        }

        private void ReturnToMainMenu()
        {
            sessionManger.EndSession();
            GameStateMachine.ChangeState<SelectLevelToEditState>();
        }

        private void OnPlayPressed()
        {
            var editSceneContext = sceneContextRegistry.GetSceneContextForScene(SceneNames.EditorSceneName);
            var workshopService = editSceneContext.Container.Resolve<IWorkshopService>();

            var levelData = workshopService.GetLevelData();

            sessionManger.EndSession();
            sessionManger.StartSession(levelData);

            GameStateMachine.ChangeState<TestWorkshopTestLevelState>();
        }

        private async UniTask LoadEditor()
        {
            await SceneManager.LoadSceneAsync(SceneNames.EditorSceneName, LoadSceneMode.Additive);
        }
    }
}