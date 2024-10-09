using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Tiles;
using UnityEngine;

namespace Pathfinding
{
    public class AstarPathfinding
    {
        private Grid2D grid;

        public void Update(Vector2Int size, Dictionary<Vector2Int, ConnectionDirection> connectionDirections)
        {
            grid = new Grid2D(size, connectionDirections);
        }

        public Vector2Int[] FindPath(Vector2Int startPos, Vector2Int targetPos)
        {
            //get player and target position in grid coords
            var seekerNode = grid.GetNodeByPos(startPos);
            var targetNode = grid.GetNodeByPos(targetPos);

            var openSet = new List<Node2D>();
            var closedSet = new HashSet<Node2D>();
            openSet.Add(seekerNode);

            //calculates path for pathfinding
            while (openSet.Count > 0) {
                //iterates through openSet and finds lowest FCost
                var node = openSet[0];
                for (var i = 1; i < openSet.Count; i++) {
                    if (openSet[i].Cost <= node.Cost) {
                        if (openSet[i].CostH < node.CostH) {
                            node = openSet[i];
                        }
                    }
                }

                openSet.Remove(node);
                closedSet.Add(node);

                //If target found, retrace path
                if (node == targetNode) {
                    var nodePath = RetracePath(seekerNode, targetNode);
                    return nodePath.Select(node2D => new Vector2Int(node2D.GridX, node2D.GridY)).ToArray();
                }

                //adds neighbor nodes to openSet
                foreach (var neighbour in grid.GetNeighbors(node)) {
                    if (!neighbour.Walkable || closedSet.Contains(neighbour)) {
                        continue;
                    }

                    var newCostToNeighbour = node.CostG + GetDistance(node, neighbour);
                    if (newCostToNeighbour >= neighbour.CostG && openSet.Contains(neighbour)) {
                        continue;
                    }

                    neighbour.CostG = newCostToNeighbour;
                    neighbour.CostH = GetDistance(neighbour, targetNode);
                    neighbour.Parent = node;

                    if (!openSet.Contains(neighbour)) {
                        openSet.Add(neighbour);
                    }
                }
            }

            return Array.Empty<Vector2Int>();
        }

        private List<Node2D> RetracePath(Node2D startNode, Node2D endNode)
        {
            var path = new List<Node2D>();
            var currentNode = endNode;

            while (currentNode != startNode) {
                path.Add(currentNode);
                currentNode = currentNode.Parent;
            }
            path.Add(startNode);
            path.Reverse();

            return path;
        }

        private int GetDistance(Node2D nodeA, Node2D nodeB)
        {
            var dstX = Mathf.Abs(nodeA.GridX - nodeB.GridX);
            var dstY = Mathf.Abs(nodeA.GridY - nodeB.GridY);

            if (dstX > dstY) {
                return 14 * dstY + 10 * (dstX - dstY);
            }
            return 14 * dstX + 10 * (dstY - dstX);
        }
    }
}