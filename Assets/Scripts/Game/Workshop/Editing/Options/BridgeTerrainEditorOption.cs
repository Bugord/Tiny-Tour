using Common.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using LevelEditor.LevelEditor.Options;
using Tiles.Ground;

namespace LevelEditing.LevelEditor.Options
{
    public class BridgeTerrainEditorOption : BaseTerrainEditorOption
    {
        public BridgeTerrainEditorOption(ITerrainLevelEditor terrainEditor, EditorOptionDataLibrary editorOptionDataLibrary) : base(terrainEditor, TerrainType.BridgeBase)
        {
            EditorOptionData = editorOptionDataLibrary.BridgeEditorOptionData;
        }
    }
}