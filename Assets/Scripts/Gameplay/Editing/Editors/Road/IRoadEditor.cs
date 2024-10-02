using System.Collections.Generic;
using Core;
using Level;
using UnityEngine;

namespace Gameplay.Editing.Editors
{
    public interface IRoadEditor
    {
        void SetRoadTile(Vector3Int position);
        void SetInitialRoadTile(Vector3Int position, ConnectionDirection connectionDirection);
        void Clear();
        bool HasRoad(Vector3Int position);
        ConnectionDirection GetRoadConnectionDirections(Vector3Int position);
        void ConnectRoads(Vector3Int positionFrom, Vector3Int positionTo);
        void EraseRoad(Vector3Int position);
        IReadOnlyCollection<RoadTileData> RoadsData { get; }
        Vector2Int RoadMapSize { get; }
        void Reset();
    }
}