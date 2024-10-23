using Core;
using Core.Navigation;
using Cysharp.Threading.Tasks;
using Game.Common.Level.Data;
using Game.Gameplay.Core;
using Game.Main.Session.Core;
using Game.Main.UI.Screens;
using Game.Main.Workshop;
using Game.Project.GameState.Systems;
using Game.Workshop.Core;
using Zenject;

namespace Game.Main.GameState.States
{
    public class EditLevelState : BaseGameState
    {
        private readonly IWorkshopService workshopService;
        private readonly INavigationService navigationService;
        private readonly SceneContextRegistry sceneContextRegistry;
        private EditLevelScreen editLevelScreen;

        private IWorkshopEditorService workshopEditorService;
        private IPlayService playService;

        private LevelData editingLevelData;
        
        public EditLevelState(GameStateMachine gameStateMachine, IWorkshopService workshopService)
            : base(gameStateMachine)
        {
            this.workshopService = workshopService;
        }

        public override void OnEnter()
        {
            workshopService.TestLevelStarted += OnTestLevelStarted;
            workshopService.LevelEditingEnded += OnLevelEditingEnded;
            
            workshopService.LoadWorkshop().Forget();
        }

        public override void OnExit()
        {
            workshopService.UnloadWorkshop();
            workshopService.TestLevelStarted -= OnTestLevelStarted;
            workshopService.LevelEditingEnded -= OnLevelEditingEnded;
        }

        private void OnTestLevelStarted()
        {
            GameStateMachine.ChangeState<TestWorkshopTestLevelState>();
        }

        private void OnLevelEditingEnded()
        {
            workshopService.Clear();
            GameStateMachine.ChangeState<SelectLevelToEditState>();
        }
    }
}