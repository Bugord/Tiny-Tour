using System.Linq;
using Common.Editors.Road;
using Gameplay.Editing.Editors;
using Gameplay.Utility;
using Pathfinding;
using UnityEngine;

namespace Gameplay.Pathfinding
{
    public class PathfindingService : IPathfindingService
    {
        private readonly IRoadEditor roadEditor;
        private readonly IRoadTilemapGridConverter roadTilemapGridConverter;
        private readonly AstarPathfinding astarPathfinding;

        public PathfindingService(IRoadEditor roadEditor, IRoadTilemapGridConverter roadTilemapGridConverter)
        {
            this.roadEditor = roadEditor;
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
            var connectionDirectionsMap = roadEditor.GetTilesData()
                .ToDictionary(data => roadTilemapGridConverter.TilemapToGrid((Vector2Int)data.position),
                    data => data.connectionDirection);

            astarPathfinding.Update(roadEditor.RoadMapSize, connectionDirectionsMap);
        }
    }
}