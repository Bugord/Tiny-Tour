using Core.LevelEditing;
using Level;
using Tiles.Ground;
using UnityEngine;

namespace Common.Editors.Terrain
{
    public interface ITerrainEditor : ILevelEditor<TerrainTileData>
    {
        void SetTerrainTile(Vector2Int position, TerrainType terrainType);
        bool HasSolidTile(Vector2Int position);
    }
}