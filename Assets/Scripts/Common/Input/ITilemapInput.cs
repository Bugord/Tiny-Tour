using System;
using UnityEngine;

namespace Common
{
    public interface ITilemapInput
    {
        event Action<Vector3Int> TilePressDown;
        event Action<Vector3Int> TilePressUp;
        event Action<Vector3Int> TileDragged;
        event Action<Vector3Int> TileAltPressDown;
        event Action<Vector3Int> TileAltPressUp;
        event Action<Vector3Int> TileAltDragged;
    }
}