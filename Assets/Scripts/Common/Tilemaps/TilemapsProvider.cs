using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.Tilemaps
{
    public class TilemapsProvider : MonoBehaviour, ITilemapsProvider, ITilemapPositionConverter
    {
        [field: SerializeField]
        public Tilemap TerrainTilemap { get; private set; }
        
        [field: SerializeField]
        public Tilemap RoadTilemap { get; private set; }

        [field: SerializeField]
        public Tilemap LogisticTilemap { get; private set; }

        [field: SerializeField]
        public Tilemap ObstacleTilemap { get; private set; }
        
        public Vector3 CellToWorld(Vector3Int cellPos)
        {
            return TerrainTilemap.GetCellCenterWorld(cellPos);
        }

        public Vector3Int WorldToCell(Vector3 pos)
        {
            return TerrainTilemap.WorldToCell(pos);
        }

        public Vector2 CellToWorld(Vector2Int cellPos) => CellToWorld((Vector3Int)cellPos);
        public Vector2Int WorldToCell(Vector2 pos) => (Vector2Int)WorldToCell((Vector3)pos);
    }
}