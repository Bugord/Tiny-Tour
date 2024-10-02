using Common.Level.Core;
using Level;
using LevelEditing.EditorState.Core;

namespace LevelEditing.EditorState.States
{
    public class LoadEditorState : BaseEditorState
    {
        private readonly LevelManager levelManager;
        private readonly ILevelLoader levelLoader;

        public LoadEditorState(EditorStateMachine editorStateMachine, LevelManager levelManager, ILevelLoader levelLoader) : base(editorStateMachine)
        {
            this.levelManager = levelManager;
            this.levelLoader = levelLoader;
        }

        public override void OnEnter()
        {
            var levelData = levelManager.GetSelectedLevel();
            levelLoader.LoadLevel(levelData);
            
            EditorStateMachine.ChangeState<EditingLevelEditorState>();
        }
    }
}