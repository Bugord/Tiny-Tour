using System;
using UnityEngine;

namespace Common
{
    public interface ITilemapInput
    {
        event Action<Vector2Int> TilePressDown;
        event Action<Vector2Int> TilePressUp;
        event Action<Vector2Int> TileDragged;
        event Action<Vector2Int> TileAltPressDown;
        event Action<Vector2Int> TileAltPressUp;
        event Action<Vector2Int> TileAltDragged;
    }
}