using System.Collections.Generic;
using Common.Tilemaps;
using Core;
using Level;
using Level.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.Editors.Logistic
{
    public class GoalEditor : IGoalEditor
    {
        private readonly ITileLibrary tileLibrary;
        private readonly Tilemap logisticTilemap;
        private readonly List<GoalData> goalsData;

        public GoalEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.tileLibrary = tileLibrary;
            goalsData = new List<GoalData>();
            
            logisticTilemap = tilemapsProvider.LogisticTilemap;
        }

        public void SetGoalTile(Vector3Int position, TeamColor teamColor)
        {
            goalsData.Add(new GoalData {
                teamColor = teamColor,
                pos = position
            });
            
            var tile = tileLibrary.GetTargetTile(teamColor);
            logisticTilemap.SetTile(position, tile);
        }
    }
}