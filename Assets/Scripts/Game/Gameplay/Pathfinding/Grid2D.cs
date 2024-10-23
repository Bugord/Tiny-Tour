using System.Collections.Generic;
using Core;
using UnityEngine;
using Utility;

namespace Game.Gameplay.Pathfinding
{
    public class Grid2D
    {
        public Node2D[,] Grid;
        public List<Node2D> Path;

        public Vector2Int Size;

        public Grid2D(Vector2Int size, Dictionary<Vector2Int, ConnectionDirection> connectionDirections)
        {
            Size = size;

            GenerateGrid(connectionDirections);
        }

        public Node2D GetNodeByPos(Vector2Int pos)
        {
            return Grid[pos.x, pos.y];
        }
        
        private void GenerateGrid(Dictionary<Vector2Int, ConnectionDirection> connectionDirections)
        {
            Grid = new Node2D[Size.x, Size.y];
            for (var x = 0; x < Size.x; x++) {
                for (var y = 0; y < Size.y; y++) {
                    if (connectionDirections.TryGetValue(new Vector2Int(x, y), out var connectionDirection)) {
                        Grid[x, y] = new Node2D(true, x, y, connectionDirection);
                    }
                    else {
                        Grid[x, y] = new Node2D(false, x, y);
                    }
                }
            }
        }

        public List<Node2D> GetNeighbors(Node2D node)
        {
            var neighbors = new List<Node2D>();

            var nodePosition = new Vector2Int(node.GridX, node.GridY);
            foreach (var neighborPosition in GridHelpers.GetNeighborPos(new Vector2Int(node.GridX, node.GridY))) {
                if (neighborPosition.x < 0 || neighborPosition.x >= Size.x || neighborPosition.y < 0 || neighborPosition.y >= Size.y) {
                    continue;
                }
                var direction = GridHelpers.GetPathDirection(nodePosition, neighborPosition);
                if (node.ConnectionDirection.HasFlag(direction)) {
                    neighbors.Add(Grid[neighborPosition.x, neighborPosition.y]);
                }
            }

            return neighbors;
        }

    }
}