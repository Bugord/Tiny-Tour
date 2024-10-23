using Game.Main.UI.Screens;
using Game.Workshop.WorkshopState.Core;
using Game.Workshop.WorkshopState.States;
using Zenject;

namespace Game.Workshop
{
    public class WorkshopEntryPoint : IInitializable
    {
        private readonly WorkshopStateMachine workshopStateMachine;

        private EditLevelScreen editLevelScreen;
        
        public WorkshopEntryPoint(WorkshopStateMachine workshopStateMachine)
        {
            this.workshopStateMachine = workshopStateMachine;
        }
        
        public void Initialize()
        {
            workshopStateMachine.ChangeState<LoadEditorState>();
        }
    }
}