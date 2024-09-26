using UnityEngine;

namespace Common.Tilemaps
{
    public interface ITilemapPositionConverter
    {
        Vector3 CellToWorld(Vector3Int cellPos);
        Vector3Int WorldToCell(Vector3 pos);
    }
}