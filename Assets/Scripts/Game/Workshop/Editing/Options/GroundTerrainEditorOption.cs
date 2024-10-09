using Common.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using LevelEditor.LevelEditor.Options;
using Tiles.Ground;

namespace LevelEditing.LevelEditor.Options
{
    public class GroundTerrainEditorOption : BaseTerrainEditorOption
    {
        public GroundTerrainEditorOption(ITerrainLevelEditor terrainLevelEditor,
            EditorOptionDataLibrary editorOptionDataLibrary) : base(terrainLevelEditor, TerrainType.Ground)
        {
            EditorOptionData = editorOptionDataLibrary.GroundEditorOptionData;
        }
    }
}