using UnityEngine;
using UnityEngine.Tilemaps;

namespace Utility
{
    public static class TilemapExtensions
    {
        public static Vector3 CellToWorldCenter(this Tilemap tilemap, Vector3Int cellPos)
        {
            return tilemap.CellToWorld(cellPos) + tilemap.cellSize / 2f;
        }
    }
}