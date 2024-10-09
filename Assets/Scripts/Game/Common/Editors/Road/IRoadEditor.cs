using System.Collections.Generic;
using Core;
using Level;
using UnityEngine;

namespace Common.Editors.Road
{
    public interface IRoadEditor
    {
        void SetRoadTile(Vector2Int position);
        void SetInitialRoadTile(Vector2Int position, ConnectionDirection connectionDirection);
        void Clear();
        bool HasTile(Vector2Int position);
        ConnectionDirection GetRoadConnectionDirections(Vector2Int position);
        void ConnectRoads(Vector2Int positionFrom, Vector2Int positionTo);
        void EraseTile(Vector2Int position);
        IReadOnlyCollection<RoadTileData> RoadsData { get; }
        Vector2Int RoadMapSize { get; }
        void Reset();
        RoadTileData[] GetRoadTiles();
    }
}