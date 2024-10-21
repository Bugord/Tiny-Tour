using Common;
using Game.Common.EditorController;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;

namespace Game.Gameplay.Editing
{
    public class PlayEditorController : BaseLevelEditorController
    {
        protected PlayEditorController(ITilemapInput tilemapInput, IEditorOptionsController editorOptionsController) : base(tilemapInput, editorOptionsController)
        {
        }

        protected override void AddOptions()
        {
            EditorOptionsController.AddOption<RoadEditorOption>();
            EditorOptionsController.AddOption<ErasingEditorOption>();

            EditorOptionsController.SelectOption<RoadEditorOption>();
        }
    }
}