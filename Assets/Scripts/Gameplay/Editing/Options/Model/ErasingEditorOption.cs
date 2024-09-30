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

        public override void OnTileDown(Vector3Int position)
        {
            if (roadEditor.HasRoad(position)) {
                roadEditor.EraseRoad(position);
            }
        }

        public override void OnTileDrag(Vector3Int position)
        {
            if (roadEditor.HasRoad(position)) {
                roadEditor.EraseRoad(position);
            }
        }
    }
}