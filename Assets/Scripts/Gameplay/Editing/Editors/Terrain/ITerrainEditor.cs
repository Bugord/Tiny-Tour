using Tiles.Ground;
using UnityEngine;

namespace Gameplay.Editing.Editors.Terrain
{
    public interface ITerrainEditor
    {
        void SetTerrainTile(Vector3Int position, TerrainType terrainType);
        void Clear();
        bool HasTileOfType(Vector3Int position, TerrainType terrainType);
    }
}