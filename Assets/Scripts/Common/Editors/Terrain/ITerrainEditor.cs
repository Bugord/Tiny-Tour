using System.Collections.Generic;
using Level;
using Tiles.Ground;
using UnityEngine;

namespace Common.Editors.Terrain
{
    public interface ITerrainEditor
    {
        IReadOnlyCollection<TerrainTileData> TerrainTilesData { get; }
        void SetInitialTile(Vector2Int position, TerrainType terrainType);
        void SetTerrainTile(Vector2Int position, TerrainType terrainType);
        bool HasTile(Vector2Int position);
        bool HasSolidTile(Vector2Int position);
        void EraseTile(Vector2Int position);
        void Reset();
        void Clear();
    }
}