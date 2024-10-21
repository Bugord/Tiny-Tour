using Core;
using Tiles;
using UnityEngine;

namespace Utility
{
    public static class Directions
    {
        public static Direction GetDirection(Vector3 from, Vector3 to) =>
            from switch {
                { } when from.y < to.y => Direction.Up,
                { } when from.y > to.y => Direction.Down,
                { } when from.x < to.x => Direction.Right,
                { } when from.x > to.x => Direction.Left,
            };
    }
}