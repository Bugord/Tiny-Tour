using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Utility
{
    public interface IRoadTilemapGridConverter
    {
        Vector2Int TilemapToGrid(Vector2Int tilemapPos);
        Vector2Int[] TilemapToGrid(IEnumerable<Vector2Int> tilemapPositions);
        Vector2Int GridToTilemap(Vector2Int gridPosition);
        Vector2Int[] GridToTilemap(IEnumerable<Vector2Int> gridPositions);
    }
}