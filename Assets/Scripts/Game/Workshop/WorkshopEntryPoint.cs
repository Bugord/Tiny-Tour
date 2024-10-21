using Game.Workshop.WorkshopState.Core;
using LevelEditing.EditorState.States;
using Zenject;

namespace LevelEditor
{
    public class WorkshopEntryPoint : IInitializable
    {
        private readonly WorkshopStateMachine workshopStateMachine;

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