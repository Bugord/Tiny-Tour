using System.Linq;
using Common.Editors.Terrain;
using Core.Logging;
using Level;

namespace Common.Editors
{
    public class TerrainService : ITerrainService
    {
        private readonly ILogger<TerrainService> logger;
        private readonly ITerrainEditor terrainEditor;

        public TerrainService(ILogger<TerrainService> logger, ITerrainEditor terrainEditor)
        {
            this.logger = logger;
            this.terrainEditor = terrainEditor;
        }

        public void LoadTerrain(TerrainTileData[] terrainTilesData)
        {
            if (terrainTilesData == null) {
                logger.LogError("Terrain tiles are null");
                return;
            }

            terrainEditor.Clear();

            foreach (var terrainTileData in terrainTilesData) {
                terrainEditor.SetInitialTile(terrainTileData.position, terrainTileData.terrainType);
            }
        }

        public TerrainTileData[] SaveTerrain()
        {
            return terrainEditor.TerrainTilesData.ToArray();
        }

        public void Reset()
        {
            terrainEditor.Reset();
        }

        //todo for editor:
        
        //public TerrainTileData[] GetTerrainTilesData()
        //{
        //    
        //}
        
        // public void SetTerrainTile(Vector3Int position, TerrainType terrainType)
        // {
        //     
        // }
        
        // public void RemoveTerrainTile(Vector3Int position)
        // {
        //     
        // }
    }
}