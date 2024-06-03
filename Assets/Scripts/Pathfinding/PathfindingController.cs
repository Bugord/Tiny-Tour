using System;
using System.Collections.Generic;
using System.Linq;
using Tiles;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Pathfinding
{
    public class PathfindingController : MonoBehaviour
    {
        [SerializeField]
        private Tilemap roadTilemap;

        private AstarPathfinding astarPathfinding;

        [SerializeField]
        public Vector2Int from;

        [SerializeField]
        public Vector2Int to;

        private void Awake()
        {
            astarPathfinding = new AstarPathfinding();
        }

        public List<Vector2Int> FindPath(Vector2Int from, Vector2Int to)
        {
            var offset = -1 * (Vector2Int)roadTilemap.cellBounds.min;
            astarPathfinding.Update(offset, (Vector2Int)roadTilemap.size, GetConnectionDirections());
            var path = astarPathfinding.FindPath(from, to);

            return path;
        }

        public Vector3[] FindPathWorld(Vector2Int from, Vector2Int to)
        {
            var path = FindPath(from, to);
            return path.Select(point => roadTilemap.CellToWorld((Vector3Int)point) + Vector3.one * 0.5f).ToArray();
        }

        [ContextMenu("PrintConnectionDirections")]
        public void PrintConnectionDirections()
        {
            Debug.Log(string.Join(", ", GetConnectionDirections().Select(x => $"({x.Key}, {x.Value})")));
        }

        [ContextMenu("FindPath")]
        public void FindPath()
        {
            var path = FindPath(from, to);
            Debug.Log(string.Join(", ", path.Select(pos => pos.ToString())));
        }

        private Dictionary<Vector2Int, ConnectionDirection> GetConnectionDirections()
        {
            var connectionDirections = new Dictionary<Vector2Int, ConnectionDirection>();
            foreach (var pos in roadTilemap.cellBounds.allPositionsWithin) {
                var roadTile = roadTilemap.GetTile<RoadTile>(pos);
                if (!roadTile) {
                    continue;
                }

                connectionDirections.Add((Vector2Int)pos, roadTile.connectionDirection);
            }

            return connectionDirections;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawCube(roadTilemap.CellToWorld((Vector3Int)from) + Vector3.one * 0.5f, Vector3.one * 0.1f);
            Gizmos.color = Color.green;
            Gizmos.DrawCube(roadTilemap.CellToWorld((Vector3Int)to) + Vector3.one * 0.5f, Vector3.one * 0.1f);
        }
    }
}