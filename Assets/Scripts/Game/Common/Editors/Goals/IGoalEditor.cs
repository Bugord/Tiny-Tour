using Core;
using Core.LevelEditing;
using Level.Data;
using UnityEngine;

namespace Common.Editors.Logistic
{
    public interface IGoalEditor
    {
        void SetTile(Vector2Int position, TeamColor teamColor);
        bool HasTile(Vector2Int position);
        void EraseTile(Vector2Int position);
        void Reset();
        void Load(GoalData[] goalsData);
        GoalData[] GetTiles();
    }
}