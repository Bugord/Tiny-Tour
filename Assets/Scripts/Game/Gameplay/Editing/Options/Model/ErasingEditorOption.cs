using Game.Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Editing.Options.Data;
using UnityEngine;

namespace Gameplay.Editing.Options
{
    public class ErasingEditorOption : BaseEditorOption
    {
        private readonly IRoadLevelEditor roadLevelEditor;

        public ErasingEditorOption(EditorOptionDataLibrary editorOptionDataLibrary, IRoadLevelEditor roadLevelEditor)
        {
            this.roadLevelEditor = roadLevelEditor;
            EditorOptionData = editorOptionDataLibrary.EraseEditorOptionData;
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