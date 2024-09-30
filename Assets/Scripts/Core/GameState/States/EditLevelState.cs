using States;
using UI.Screens;

namespace Core.GameState.States
{
    public class EditLevelState : BaseGameState
    {
        private EditLevelScreen editLevelScreen;

        public EditLevelState(GameStateMachine gameStateSystem) : base(gameStateSystem)
        {
        }
    }
}