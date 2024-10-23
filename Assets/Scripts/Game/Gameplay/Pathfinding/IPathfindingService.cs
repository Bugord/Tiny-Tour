using UnityEngine;

namespace Game.Gameplay.Pathfinding
{
    public interface IPathfindingService
    {
        Vector2Int[] FindPath(Vector2Int from, Vector2Int to);
    }
}