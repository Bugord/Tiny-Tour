using Common.Editors;
using Core;
using UnityEngine;

namespace Game.Gameplay.Logistic
{
    public interface ILogisticService : ILogisticLoader
    {
        void AddGoal(Vector2Int position, TeamColor teamColor);
        Vector2[] GetPathForCar(Vector2 carPos, TeamColor carTeamColor);
        void Clear();
    }
}