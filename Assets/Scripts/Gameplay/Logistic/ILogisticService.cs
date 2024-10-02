using Core;
using Level.Data;
using UnityEngine;

namespace Gameplay.Logistic
{
    public interface ILogisticService
    {
        void AddGoal(Vector2Int position, TeamColor teamColor);
        Vector2[] GetPathForCar(Vector2 carPos, TeamColor carTeamColor);
        void Reset();
        void LoadLogistic(LogisticData logisticData);
    }
}