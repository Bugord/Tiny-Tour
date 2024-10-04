using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Core;
using Core.Logging;
using Level;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Common.Editors.Road
{
    public class RoadEditor : IRoadEditor
    {
        private readonly ILogger<RoadEditor> logger;
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap roadTilemap;

        private readonly List<RoadTileData> initialRoadsData;
        private readonly List<RoadTileData> roadsData;

        public IReadOnlyCollection<RoadTileData> RoadsData => roadsData;
        public Vector2Int RoadMapSize => (Vector2Int)roadTilemap.size;

        public RoadEditor(ILogger<RoadEditor> logger, ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.logger = logger;
            this.tileLibrary = tileLibrary;
            roadTilemap = tilemapsProvider.RoadTilemap;

            initialRoadsData = new List<RoadTileData>();
            roadsData = new List<RoadTileData>();
        }

        public void Reset()
        {
            foreach (var roadTileData in roadsData) {
                roadTilemap.SetTile((Vector3Int)roadTileData.position, null);
            }
            roadsData.Clear();

            foreach (var initialRoadData in initialRoadsData) {
                roadsData.Add(new RoadTileData {
                    position = initialRoadData.position,
                    connectionDirection = initialRoadData.connectionDirection
                });
                var tile = tileLibrary.GetRoadTile(initialRoadData.connectionDirection);
                roadTilemap.SetTile((Vector3Int)initialRoadData.position, tile);
            }
        }

        public RoadTileData[] GetRoadTiles()
        {
            return roadsData.ToArray();
        }

        public void SetRoadTile(Vector2Int position)
        {
            var existingRoadData = roadsData.FirstOrDefault(data => data.position == position);
            if (existingRoadData != null) {
                logger.LogWarning($"Road at {position} already exists");
                return;
            }

            var roadData = new RoadTileData {
                position = position,
                connectionDirection = ConnectionDirection.None
            };
            roadsData.Add(roadData);

            var tile = tileLibrary.GetRoadTile(roadData.connectionDirection);
            roadTilemap.SetTile((Vector3Int)position, tile);
        }

        public bool HasTile(Vector2Int position)
        {
            return roadsData.Any(data => data.position == position);
        }

        public void EraseTile(Vector2Int position)
        {
            var existingRoadData = roadsData.FirstOrDefault(data => data.position == position);
            if (existingRoadData == null) {
                logger.LogWarning($"Cant erase road at {position} because it is null");
                return;
            }
            var initialRoadData = initialRoadsData.FirstOrDefault(data => data.position == position);

            var neighbourPositions = GridHelpers.GetNeighborPos(position);
            var neighbourRoadsData = roadsData.Where(data => neighbourPositions.Contains(data.position));
            foreach (var neighbourRoadData in neighbourRoadsData) {
                var neighbourInitialRoadData =
                    initialRoadsData.FirstOrDefault(data => data.position == neighbourRoadData.position);
                var neighbourConnection = GridHelpers.GetPathDirection(neighbourRoadData.position, position);
                if (neighbourInitialRoadData != null && neighbourInitialRoadData.HasDirection(neighbourConnection)) {
                    continue;
                }

                if (initialRoadData != null) {
                    var directionToNeighbour = GridHelpers.GetPathDirection(position, neighbourRoadData.position);
                    if (!initialRoadData.HasDirection(directionToNeighbour)) {
                        existingRoadData.TurnOffDirection(directionToNeighbour);
                    }
                }

                neighbourRoadData.TurnOffDirection(neighbourConnection);
                var tile = tileLibrary.GetRoadTile(neighbourRoadData.connectionDirection);
                roadTilemap.SetTile((Vector3Int)neighbourRoadData.position, tile);
            }

            if (initialRoadData == null) {
                roadsData.Remove(existingRoadData);
                roadTilemap.SetTile((Vector3Int)position, null);
            }
            else {
                var tile = tileLibrary.GetRoadTile(existingRoadData.connectionDirection);
                roadTilemap.SetTile((Vector3Int)position, tile);
            }
        }

        public ConnectionDirection GetRoadConnectionDirections(Vector2Int position)
        {
            var road = roadsData.FirstOrDefault(data => data.position == position);
            if (road == null) {
                throw new ArgumentException($"Trying to get connection direction but road is null on pos {position}");
            }

            return road.connectionDirection;
        }

        public void ConnectRoads(Vector2Int positionFrom, Vector2Int positionTo)
        {
            var roadFrom = roadsData.FirstOrDefault(data => data.position == positionFrom);
            if (roadFrom == null) {
                throw new ArgumentException($"Cannot connect roads, road from ({positionFrom}) is null");
            }

            var roadTo = roadsData.FirstOrDefault(data => data.position == positionTo);
            if (roadTo == null) {
                throw new ArgumentException($"Cannot connect road, road to ({positionFrom}) is null");
            }

            roadFrom.TurnOnDirection(GridHelpers.GetPathDirection(positionFrom, positionTo));
            roadTo.TurnOnDirection(GridHelpers.GetPathDirection(positionTo, positionFrom));

            var tileFrom = tileLibrary.GetRoadTile(roadFrom.connectionDirection);
            roadTilemap.SetTile((Vector3Int)positionFrom, tileFrom);

            var tileTo = tileLibrary.GetRoadTile(roadTo.connectionDirection);
            roadTilemap.SetTile((Vector3Int)positionTo, tileTo);
        }

        public void SetInitialRoadTile(Vector2Int position, ConnectionDirection connectionDirection)
        {
            var initialRoadTileData = new RoadTileData {
                position = position,
                connectionDirection = connectionDirection
            };
            initialRoadsData.Add(initialRoadTileData);

            var roadTileData = new RoadTileData {
                position = position,
                connectionDirection = connectionDirection
            };
            roadsData.Add(roadTileData);

            var tile = tileLibrary.GetRoadTile(connectionDirection);
            roadTilemap.SetTile((Vector3Int)position, tile);
        }

        public void Clear()
        {
            initialRoadsData.Clear();
            roadsData.Clear();
            roadTilemap.ClearAllTiles();
        }
    }
}