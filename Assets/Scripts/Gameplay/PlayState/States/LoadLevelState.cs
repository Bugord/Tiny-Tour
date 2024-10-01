using Gameplay.PlayState.Core;
using Level;

namespace Gameplay.PlayState.States
{
    public class LoadLevelState : BasePlayState
    {
        private readonly LevelManager levelManager;
        private readonly ILevelLoader levelLoader;

        public LoadLevelState(PlayStateMachine playStateMachine, LevelManager levelManager, ILevelLoader levelLoader)
            : base(playStateMachine)
        {
            this.levelManager = levelManager;
            this.levelLoader = levelLoader;
        }

        public override void OnEnter()
        {
            LoadLevel();
            PlayStateMachine.ChangeState<EditingLevelState>();
        }

        private void LoadLevel()
        {
            var level = levelManager.GetSelectedLevel();
            levelLoader.LoadLevel(level);
        }
    }
}