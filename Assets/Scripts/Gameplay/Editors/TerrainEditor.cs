using Common.Tilemaps;
using Core.Logging;
using Level;
using UnityEngine.Tilemaps;

namespace Common
{
    public class TerrainEditor : ITerrainEditor
    {
        private readonly ILogger<TerrainEditor> logger;
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap terrainTilemap;
        
        public TerrainEditor(ILogger<TerrainEditor> logger, ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            terrainTilemap = tilemapsProvider.TerrainTilemap;
            this.logger = logger;
            this.tileLibrary = tileLibrary;
        }

        public void LoadTerrain(TerrainTileData[] terrainTilesData)
        {
            terrainTilemap.ClearAllTiles();
            
            if (terrainTilesData == null) {
                logger.LogError("Terrain tiles are null");
                return;
            }

            foreach (var terrainTileData in terrainTilesData) {
                terrainTilemap.SetTile(terrainTileData.position,
                    tileLibrary.GetTerrainTileByType(terrainTileData.terrainType));
            }
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