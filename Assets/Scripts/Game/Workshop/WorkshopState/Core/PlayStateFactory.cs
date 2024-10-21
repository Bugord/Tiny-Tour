using LevelEditing.EditorState.States;
using Zenject;

namespace Game.Workshop.WorkshopState.Core
{
    public class WorkshopStateFactory : IWorkshopStateFactory
    {
        private readonly DiContainer diContainer;

        public WorkshopStateFactory(DiContainer diContainer)
        {
            this.diContainer = diContainer;
        }

        public T Create<T>(WorkshopStateMachine workshopStateMachine) where T : BaseEditorState
        {
            return diContainer.Instantiate<T>(new[] { workshopStateMachine });
        }
    }
}