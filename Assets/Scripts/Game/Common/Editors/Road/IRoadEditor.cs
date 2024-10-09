using System.Collections.Generic;
using Core;
using Core.LevelEditing;
using Level;
using UnityEngine;

namespace Common.Editors.Road
{
    public interface IRoadEditor : ILevelEditor<RoadTileData>
    {
        void SetRoadTile(Vector2Int position);
        void ConnectRoads(Vector2Int positionFrom, Vector2Int positionTo);
        Vector2Int RoadMapSize { get; }
    }
}