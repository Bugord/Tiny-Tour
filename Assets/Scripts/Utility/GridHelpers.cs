using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace Utility
{
    public static class GridHelpers
    {
        public static ConnectionDirection GetPathDirection(Vector2Int from, Vector2Int to) =>
            from switch {
                { } when from.y < to.y => ConnectionDirection.Up,
                { } when from.y > to.y => ConnectionDirection.Down,
                { } when from.x < to.x => ConnectionDirection.Right,
                { } when from.x > to.x => ConnectionDirection.Left,
                _ => ConnectionDirection.None
            };

        public static IEnumerable<Vector2Int> GetNeighborPos(Vector2Int pos)
        {
            yield return pos + Vector2Int.up;
            yield return pos + Vector2Int.down;
            yield return pos + Vector2Int.left;
            yield return pos + Vector2Int.right;
        }
        
        public static ConnectionDirection GetPathDirection(Vector3Int from, Vector3Int to) =>
            from switch {
                { } when from.y < to.y => ConnectionDirection.Up,
                { } when from.y > to.y => ConnectionDirection.Down,
                { } when from.x < to.x => ConnectionDirection.Right,
                { } when from.x > to.x => ConnectionDirection.Left,
                _ => ConnectionDirection.None
            };

        public static IEnumerable<Vector3Int> GetNeighborPos(Vector3Int pos)
        {
            yield return pos + Vector3Int.up;
            yield return pos + Vector3Int.down;
            yield return pos + Vector3Int.left;
            yield return pos + Vector3Int.right;
        }
    }
}