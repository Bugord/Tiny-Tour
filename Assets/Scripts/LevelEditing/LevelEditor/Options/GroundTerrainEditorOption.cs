using Common.Editors.Terrain;
using Gameplay.Editing.Options.Data;
using LevelEditor.LevelEditor.Options;
using Tiles.Ground;

namespace LevelEditing.LevelEditor.Options
{
    public class GroundTerrainEditorOption : BaseTerrainEditorOption
    {
        protected override TerrainType TerrainType => TerrainType.Ground;

        public GroundTerrainEditorOption(ITerrainEditor terrainEditor, EditorOptionDataLibrary editorOptionDataLibrary) : base(terrainEditor)
        {
            EditorOptionData = editorOptionDataLibrary.GroundEditorOptionData;
        }
    }
}