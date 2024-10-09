using System.Linq;
using Common.Editors.Terrain;
using Core.Logging;
using Level;

namespace Common.Editors
{
    public class TerrainService : ITerrainService
    {
        private readonly ILogger<TerrainService> logger;
        private readonly ITerrainLevelEditor terrainLevelEditor;

        public TerrainService(ILogger<TerrainService> logger, ITerrainLevelEditor terrainLevelEditor)
        {
            this.logger = logger;
            this.terrainLevelEditor = terrainLevelEditor;
        }

        public void LoadTerrain(TerrainTileData[] terrainTilesData)
        {
            if (terrainTilesData == null) {
                logger.LogError("Terrain tiles are null");
                return;
            }

            terrainLevelEditor.Clear();
            terrainLevelEditor.Load(terrainTilesData);
        }

        public TerrainTileData[] SaveTerrain()
        {
            return terrainLevelEditor.GetTilesData();
        }

        public void Reset()
        {
            terrainLevelEditor.Reset();
        }
    }
}