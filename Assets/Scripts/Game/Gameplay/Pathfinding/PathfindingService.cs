using System.Linq;
using Game.Common.Editors.Road;
using Gameplay.Utility;
using Pathfinding;
using UnityEngine;

namespace Gameplay.Pathfinding
{
    public class PathfindingService : IPathfindingService
    {
        private readonly IRoadLevelEditor roadLevelEditor;
        private readonly IRoadTilemapGridConverter roadTilemapGridConverter;
        private readonly AstarPathfinding astarPathfinding;

        public PathfindingService(IRoadLevelEditor roadLevelEditor, IRoadTilemapGridConverter roadTilemapGridConverter)
        {
            this.roadLevelEditor = roadLevelEditor;
            this.roadTilemapGridConverter = roadTilemapGridConverter;
            astarPathfinding = new AstarPathfinding();
        }

        public Vector2Int[] FindPath(Vector2Int from, Vector2Int to)
        {
            UpdateGrid();
            
            var gridFrom = roadTilemapGridConverter.TilemapToGrid(from);
            var gridTo = roadTilemapGridConverter.TilemapToGrid(to);
            
            var gridPath = astarPathfinding.FindPath(gridFrom, gridTo);
            var tilemapPath = roadTilemapGridConverter.GridToTilemap(gridPath);

            return tilemapPath;
        }

        private void UpdateGrid()
        {
            var connectionDirectionsMap = roadLevelEditor.GetTilesData()
                .ToDictionary(data => roadTilemapGridConverter.TilemapToGrid((Vector2Int)data.position),
                    data => data.connectionDirection);

            astarPathfinding.Update(roadLevelEditor.RoadMapSize, connectionDirectionsMap);
        }
    }
}