using Common.Level.Core;
using Level;
using LevelEditing.EditorState.Core;
using LevelEditor.Level.Core;

namespace LevelEditing.EditorState.States
{
    public class LoadEditorState : BaseEditorState
    {
        private readonly IWorkshopService workshopService;

        public LoadEditorState(EditorStateMachine editorStateMachine, IWorkshopService workshopService) : base(editorStateMachine)
        {
            this.workshopService = workshopService;
        }

        public override void OnEnter()
        {
            workshopService.LoadCurrentLevel();
            EditorStateMachine.ChangeState<EditingLevelEditorState>();
        }
    }
}