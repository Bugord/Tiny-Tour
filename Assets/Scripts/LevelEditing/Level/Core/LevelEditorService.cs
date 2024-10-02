using Common.Editors;
using Level.Data;

namespace LevelEditor.Level.Core
{
    public class LevelEditorService : ILevelEditorService
    {
        private readonly ITerrainLoader terrainLoader;
        private readonly IRoadService roadService;

        public LevelEditorService(ITerrainLoader terrainLoader, IRoadService roadService)
        {
            this.terrainLoader = terrainLoader;
            this.roadService = roadService;
        }

        public void LoadLevel(LevelData levelData)
        {
            terrainLoader.LoadTerrain(levelData.terrainTilesData);
            roadService.LoadRoad(levelData.logisticData.roadTileData);
        }

        public void ResetLevel()
        {
            roadService.Reset();
        }
    }
}