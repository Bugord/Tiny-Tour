using Cars;
using Core;
using Level.Data;
using UnityEngine;

namespace LevelEditing.Editing.Editors
{
    public interface ISpawnPointEditor
    {
        void Load(CarSpawnData[] carsSpawnData);
        void SetCarSpawnPoint(Vector2Int position, CarType carType, TeamColor teamColor, Direction direction);
        void EraseTile(Vector2Int position);
        void Reset();
        void Clear();
        CarSpawnData[] GetCarsSpawnData();
        bool HasTile(Vector2Int position);
    }
}