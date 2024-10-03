using System.Collections.Generic;
using Level;
using Tiles.Ground;
using UnityEngine;

namespace Common.Editors.Terrain
{
    public interface ITerrainEditor
    {
        IReadOnlyCollection<TerrainTileData> TerrainTilesData { get; }
        void SetTerrainTile(Vector3Int position, TerrainType terrainType);
        bool HasSolidTile(Vector3Int position);
    }
}