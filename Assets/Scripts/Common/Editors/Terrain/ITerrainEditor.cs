using System.Collections.Generic;
using Level;
using Tiles.Ground;
using UnityEngine;

namespace Common.Editors.Terrain
{
    public interface ITerrainEditor
    {
        IReadOnlyCollection<TerrainTileData> TerrainTilesData { get; }
        void SetInitialTile(Vector3Int position, TerrainType terrainType);
        void SetTerrainTile(Vector3Int position, TerrainType terrainType);
        bool HasTile(Vector3Int position);
        bool HasSolidTile(Vector3Int position);
        void EraseTile(Vector3Int position);
        void Reset();
        void Clear();
    }
}