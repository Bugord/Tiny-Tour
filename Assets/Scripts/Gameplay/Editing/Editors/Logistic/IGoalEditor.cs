using Core;
using UnityEngine;

namespace Common.Editors.Logistic
{
    public interface IGoalEditor
    {
        void SetGoalTile(Vector3Int position, TeamColor teamColor);
    }
}