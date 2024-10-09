using Common.Level.Core;
using Level;
using LevelEditing.EditorState.Core;
using LevelEditor.Level.Core;

namespace LevelEditing.EditorState.States
{
    public class LoadEditorState : BaseEditorState
    {
        private readonly ILevelEditorService levelEditorService;

        public LoadEditorState(EditorStateMachine editorStateMachine, ILevelEditorService levelEditorService) : base(editorStateMachine)
        {
            this.levelEditorService = levelEditorService;
        }

        public override void OnEnter()
        {
            levelEditorService.LoadCurrentLevel();
            EditorStateMachine.ChangeState<EditingLevelEditorState>();
        }
    }
}