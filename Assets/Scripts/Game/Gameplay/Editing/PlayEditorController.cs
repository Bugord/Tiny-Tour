using Common;
using Game.Common.EditorController;
using Game.Common.EditorOptions;
using Game.Gameplay.Editing.Options.Model;
using UnityEngine;

namespace Game.Gameplay.Editing
{
    public class PlayEditorController : BaseLevelEditorController
    {
        private BaseEditorOption cachedEditorOption;

        protected PlayEditorController(ITilemapInput tilemapInput, IEditorOptionsController editorOptionsController) : base(tilemapInput, editorOptionsController)
        {
        }

        protected override void AddOptions()
        {
            EditorOptionsController.AddOption<RoadEditorOption>();
            EditorOptionsController.AddOption<ErasingEditorOption>();

            EditorOptionsController.SelectOption<RoadEditorOption>();
        }
        
        protected override void OnTileAltDown(Vector2Int tilePos)
        {
            cachedEditorOption = EditorOptionsController.SelectedOption;
            EditorOptionsController.SelectOption<ErasingEditorOption>();

            base.OnTileAltDown(tilePos);
        }

        protected override void OnTileAltUp(Vector2Int tilePos)
        {
            base.OnTileAltUp(tilePos);

            EditorOptionsController.SelectOption(cachedEditorOption);
            cachedEditorOption = null;
        }
    }
}