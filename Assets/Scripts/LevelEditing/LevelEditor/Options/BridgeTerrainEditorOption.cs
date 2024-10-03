using Common.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using LevelEditor.LevelEditor.Options;
using Tiles.Ground;

namespace LevelEditing.LevelEditor.Options
{
    public class BridgeTerrainEditorOption : BaseTerrainEditorOption
    {
        protected override TerrainType TerrainType => TerrainType.BridgeBase;

        public BridgeTerrainEditorOption(ITerrainEditor terrainEditor, EditorOptionDataLibrary editorOptionDataLibrary) : base(terrainEditor)
        {
            EditorOptionData = editorOptionDataLibrary.BridgeEditorOptionData;
        }
    }
}