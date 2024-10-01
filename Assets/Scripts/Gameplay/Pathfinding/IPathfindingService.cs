using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public interface IPathfindingService
    {
        Vector2Int[] FindPath(Vector2Int from, Vector2Int to);
    }
}