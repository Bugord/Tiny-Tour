using Cysharp.Threading.Tasks;
using Game.Main.Workshop;
using Game.Project.GameState.Systems;
using Unity.VisualScripting;

namespace Game.Main.GameState.States
{
    public class TestWorkshopTestLevelState : BaseGameState
    {
        private readonly IWorkshopService workshopService;

        public TestWorkshopTestLevelState(GameStateMachine gameStateMachine, IWorkshopService workshopService)
            : base(gameStateMachine)
        {
            this.workshopService = workshopService;
        }

        public override void OnEnter()
        {
            workshopService.LoadLevelTest().Forget();
            workshopService.TestLevelEnded += OnTestLevelEnded;
        }

        public override void OnExit()
        {
            workshopService.TestLevelEnded -= OnTestLevelEnded;
        }

        private void OnTestLevelEnded()
        {
            workshopService.UnloadLevelTest();
            GameStateMachine.ChangeState<EditLevelState>();
        }
    }
}