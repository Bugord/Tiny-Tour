using Core.Logging;
using Gameplay.Editing.Editors.Terrain;
using Level;

namespace Common.Editors
{
    public class TerrainLoader : ITerrainLoader
    {
        private readonly ILogger<TerrainLoader> logger;
        private readonly ITerrainEditor terrainEditor;

        public TerrainLoader(ILogger<TerrainLoader> logger, ITerrainEditor terrainEditor)
        {
            this.logger = logger;
            this.terrainEditor = terrainEditor;
        }

        public void LoadTerrain(TerrainTileData[] terrainTilesData)
        {
            terrainEditor.Clear();
            
            if (terrainTilesData == null) {
                logger.LogError("Terrain tiles are null");
                return;
            }

            foreach (var terrainTileData in terrainTilesData) {
                terrainEditor.SetTerrainTile(terrainTileData.position, terrainTileData.terrainType);
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