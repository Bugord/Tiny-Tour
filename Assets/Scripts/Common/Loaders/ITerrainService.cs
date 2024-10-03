using Level;

namespace Common.Editors
{
    public interface ITerrainService
    {
        void LoadTerrain(TerrainTileData[] terrainTilesData);
        TerrainTileData[] SaveTerrain();
        void Reset();
    }
}