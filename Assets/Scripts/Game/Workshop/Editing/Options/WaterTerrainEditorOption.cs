using Common.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using LevelEditor.LevelEditor.Options;
using Tiles.Ground;

namespace LevelEditing.LevelEditor.Options
{
    public class WaterTerrainEditorOption : BaseTerrainEditorOption
    {
        public WaterTerrainEditorOption(ITerrainLevelEditor terrainLevelEditor,
            EditorOptionDataLibrary editorOptionDataLibrary) : base(terrainLevelEditor, TerrainType.Water)
        {
            EditorOptionData = editorOptionDataLibrary.WaterEditorOptionData;
        }
    }
}