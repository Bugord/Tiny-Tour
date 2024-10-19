using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Model
{
    public class ErasingEditorOption : BaseEditorOption
    {
        private readonly IRoadLevelEditor roadLevelEditor;

        public ErasingEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IRoadLevelEditor roadLevelEditor)
            : base(editorOptionUI, editorOptionDataLibrary.EraseEditorOptionData)
        {
            this.roadLevelEditor = roadLevelEditor;

            editorOptionUI.SetBorders(editorOptionDataLibrary.EraseEditorOptionData.ActiveBorderSprite,
                editorOptionDataLibrary.EraseEditorOptionData.InactiveBorderSprite);
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (roadLevelEditor.HasTile(position)) {
                roadLevelEditor.EraseTile(position);
            }
        }

        public override void OnTileDrag(Vector2Int position)
        {
            if (roadLevelEditor.HasTile(position)) {
                roadLevelEditor.EraseTile(position);
            }
        }
    }
}