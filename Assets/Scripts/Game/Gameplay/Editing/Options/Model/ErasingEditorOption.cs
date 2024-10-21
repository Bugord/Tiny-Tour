using Game.Common.Editors.Road;
using Game.Common.UI.Editing.EditorOption;
using Game.Gameplay.Editing.Options.Data;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Model
{
    public class ErasingEditorOption : BaseEditorOption
    {
        private readonly IRoadEditor roadEditor;

        public ErasingEditorOption(EditorOptionUI editorOptionUI, EditorOptionDataLibrary editorOptionDataLibrary,
            IRoadEditor roadEditor)
            : base(editorOptionUI, editorOptionDataLibrary.EraseEditorOptionData)
        {
            this.roadEditor = roadEditor;

            editorOptionUI.SetBorders(editorOptionDataLibrary.EraseEditorOptionData.ActiveBorderSprite,
                editorOptionDataLibrary.EraseEditorOptionData.InactiveBorderSprite);
        }

        public override void OnTileDown(Vector2Int position)
        {
            if (roadEditor.HasTile(position)) {
                roadEditor.EraseTile(position);
            }
        }

        public override void OnTileDrag(Vector2Int position)
        {
            if (roadEditor.HasTile(position)) {
                roadEditor.EraseTile(position);
            }
        }
    }
}