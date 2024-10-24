﻿using Core;
using Core.LevelEditing;
using Game.Common.Cars.Core;
using Game.Common.Level.Data;
using UnityEngine;

namespace Game.Workshop.Editing.Editors
{
    public interface ISpawnPointEditor : ILevelEditor<CarSpawnData>
    {
        void SetCarSpawnPoint(Vector2Int position, CarType carType, TeamColor teamColor, Direction direction);
        bool HasSpawnPoint(Vector2Int position, CarType carType, TeamColor color);
        void RotateSpawnPoint(Vector2Int position);
    }
}