using Core;
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
    }
}