using LevelEditing.EditorState.States;

namespace Game.Workshop.WorkshopState.Core
{
    public interface IWorkshopStateFactory
    {
        T Create<T>(WorkshopStateMachine workshopStateMachine) where T : BaseEditorState;
    }
}