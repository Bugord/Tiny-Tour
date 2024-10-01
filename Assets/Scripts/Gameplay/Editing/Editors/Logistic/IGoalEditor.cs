using Core;
using UnityEngine;

namespace Common.Editors.Logistic
{
    public interface IGoalEditor
    {
        void SetGoalTile(Vector2Int position, TeamColor teamColor);
    }
}