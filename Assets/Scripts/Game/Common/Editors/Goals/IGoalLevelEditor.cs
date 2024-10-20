using Core;
using Core.LevelEditing;
using Game.Common.Level.Data;
using UnityEngine;

namespace Game.Common.Editors.Goals
{
    public interface IGoalLevelEditor : ILevelEditor<GoalData>
    {
        void SetTile(Vector2Int position, TeamColor teamColor);
    }
}