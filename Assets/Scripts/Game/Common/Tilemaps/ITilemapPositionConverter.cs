using UnityEngine;

namespace Common.Tilemaps
{
    public interface ITilemapPositionConverter
    {
        Vector3 CellToWorld(Vector3Int cellPos);
        Vector2Int WorldToCell(Vector3 pos); 
        Vector2 CellToWorld(Vector2Int cellPos);
        Vector2Int WorldToCell(Vector2 pos);
    }
}