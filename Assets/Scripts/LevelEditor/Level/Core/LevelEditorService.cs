using Common.Editors;
using Common.Level.Core;
using Level.Data;

namespace LevelEditor.Level.Core
{
    public class LevelEditorService : ILevelLoader
    {
        private readonly ITerrainLoader terrainLoader;

        public LevelEditorService(ITerrainLoader terrainLoader)
        {
            this.terrainLoader = terrainLoader;
        }

        public void LoadLevel(LevelData levelData)
        {
            terrainLoader.LoadTerrain(levelData.terrainTilesData);
        }
    }
}