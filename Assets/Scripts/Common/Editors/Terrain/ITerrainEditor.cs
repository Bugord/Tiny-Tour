using Tiles.Ground;
using UnityEngine;

namespace Common.Editors.Terrain
{
    public interface ITerrainEditor
    {
        void SetTerrainTile(Vector3Int position, TerrainType terrainType);
        void Clear();
        bool HasSolidTile(Vector3Int position);
    }
}