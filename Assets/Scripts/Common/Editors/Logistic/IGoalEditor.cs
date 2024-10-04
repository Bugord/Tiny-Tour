using Core;
using Level.Data;
using UnityEngine;

namespace Common.Editors.Logistic
{
    public interface IGoalEditor
    {
        void Load(GoalData[] goalsData);
        void SetGoalTile(Vector2Int position, TeamColor teamColor);
        void EraseTile(Vector2Int position);
        void Reset();
        void Clear();
        GoalData[] GetGoalPoints();
        bool HasTile(Vector2Int position);
    }
}