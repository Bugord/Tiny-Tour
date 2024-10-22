using Game.Gameplay.Level;
using Game.Gameplay.PlayState.Core;
using Game.Main.Session.Core;

namespace Game.Gameplay.PlayState.States
{
    public class LoadLevelState : BasePlayState
    {
        private readonly ISessionManger sessionManger;
        private readonly ILevelService levelService;

        public LoadLevelState(PlayStateMachine playStateMachine, ISessionManger sessionManger, ILevelService levelService)
            : base(playStateMachine)
        {
            this.sessionManger = sessionManger;
            this.levelService = levelService;
        }

        public override void OnEnter()
        {
            LoadLevel();
            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private void LoadLevel()
        {
            var level = sessionManger.CurrentSession.LevelData;
            levelService.LoadLevel(level);
        }
    }
}