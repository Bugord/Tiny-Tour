using Common.Tilemaps;
using Core;
using Level;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.Editors.Logistic
{
    public class GoalEditor : IGoalEditor
    {
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap logisticTilemap;

        public GoalEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.tileLibrary = tileLibrary;
            logisticTilemap = tilemapsProvider.LogisticTilemap;
        }

        public void SetGoalTile(Vector2Int position, TeamColor teamColor)
        {
            var tile = tileLibrary.GetTargetTile(teamColor);
            logisticTilemap.SetTile((Vector3Int)position, tile);
        }
    }
}