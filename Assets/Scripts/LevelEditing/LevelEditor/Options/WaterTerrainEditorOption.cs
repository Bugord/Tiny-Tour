using Common.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using LevelEditor.LevelEditor.Options;
using Tiles.Ground;

namespace LevelEditing.LevelEditor.Options
{
    public class WaterTerrainEditorOption : BaseTerrainEditorOption
    {
        protected override TerrainType TerrainType => TerrainType.Water;

        public WaterTerrainEditorOption(ITerrainEditor terrainEditor, EditorOptionDataLibrary editorOptionDataLibrary) : base(terrainEditor)
        {
            EditorOptionData = editorOptionDataLibrary.WaterEditorOptionData;
        }
    }
}