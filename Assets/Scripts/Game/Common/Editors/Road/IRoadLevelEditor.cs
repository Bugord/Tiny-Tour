using Core.LevelEditing;
using Level;
using UnityEngine;

namespace Game.Common.Editors.Road
{
    public interface IRoadLevelEditor : ILevelEditor<RoadTileData>
    {
        void SetRoadTile(Vector2Int position);
        void ConnectRoads(Vector2Int positionFrom, Vector2Int positionTo);
        Vector2Int RoadMapSize { get; }
    }
}