using Common.Level.Core;
using Gameplay.PlayState.Core;
using Level;

namespace Gameplay.PlayState.States
{
    public class LoadLevelState : BasePlayState
    {
        private readonly LevelManager levelManager;
        private readonly ILevelService levelService;

        public LoadLevelState(PlayStateMachine playStateMachine, LevelManager levelManager, ILevelService levelService)
            : base(playStateMachine)
        {
            this.levelManager = levelManager;
            this.levelService = levelService;
        }

        public override void OnEnter()
        {
            LoadLevel();
            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private void LoadLevel()
        {
            var level = levelManager.GetSelectedLevel();
            levelService.LoadLevel(level);
        }
    }
}