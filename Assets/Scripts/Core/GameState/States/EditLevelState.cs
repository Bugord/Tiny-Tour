using States;
using UI.Screens;

namespace Core.GameState.States
{
    public class EditLevelState : BaseGameState
    {
        private readonly GameStateMachine gameStateSystem;

        private EditLevelScreen editLevelScreen;

        public EditLevelState(GameStateMachine gameStateSystem)
        {
            this.gameStateSystem = gameStateSystem;
        }
    }
}