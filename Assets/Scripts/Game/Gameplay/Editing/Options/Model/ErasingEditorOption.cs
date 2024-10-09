using Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using UnityEngine;

namespace Gameplay.Editing.Options
{
    public class ErasingEditorOption : BaseEditorOption
    {
        private readonly IRoadEditor roadEditor;

        public ErasingEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadEditor roadEditor)
        {
            this.roadEditor = roadEditor;
            EditorOptionData = editorOptionDataLibrary.EraseEditorOptionData;
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