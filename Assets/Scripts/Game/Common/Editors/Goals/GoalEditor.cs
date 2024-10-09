using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Core;
using Core.Logging;
using Level;
using Level.Data;
using LevelEditing.Editing.Editors;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Common.Editors.Logistic
{
    public class GoalEditor : IGoalEditor
    {
        private readonly ILogger<GoalEditor> logger;
        private readonly Tilemap logisticTilemap;
        private readonly ITileLibrary tileLibrary;
        private readonly Dictionary<Vector2Int, GoalData> initialGoalsData;
        private readonly Dictionary<Vector2Int, GoalData> goalsData;

        private const int GoalLayer = 2;

        public GoalEditor(ILogger<GoalEditor> logger, ITilemapsProvider tilemapsProvider,
            ITileLibrary tileLibrary)
        {
            this.logger = logger;
            this.tileLibrary = tileLibrary;
            logisticTilemap = tilemapsProvider.LogisticTilemap;

            initialGoalsData = new Dictionary<Vector2Int, GoalData>();
            goalsData = new Dictionary<Vector2Int, GoalData>();
        }

        public void Load(GoalData[] goalsData)
        {
            Clear();

            foreach (var goalData in goalsData) {
                if (initialGoalsData.ContainsKey(goalData.position)) {
                    logger.LogWarning($"Trying to load more that 1 goal at same position {goalData.position}");
                    continue;
                }

                initialGoalsData.Add(goalData.position, goalData);
                SetTile(goalData.position, goalData.teamColor);
            }
        }

        public void SetTile(Vector2Int position, TeamColor teamColor)
        {
            if (goalsData.TryGetValue(position, out var existingGoalData)) {
                goalsData.Remove(existingGoalData.position);
            }

            var carGoalData = new GoalData(position, teamColor);
            goalsData.Add(carGoalData.position, carGoalData);
            SetTile(carGoalData);
        }

        public void EraseTile(Vector2Int position)
        {
            goalsData.Remove(position);
            var tilePosition = GetTilePosition(position);
            logisticTilemap.SetTile(tilePosition, null);
        }

        public void Reset()
        {
            ClearTilemap();
            goalsData.Clear();

            foreach (var initialCarGoalPoint in initialGoalsData.Values) {
                SetTile(initialCarGoalPoint.position, initialCarGoalPoint.teamColor);
            }
        }

        public void Clear()
        {
            ClearTilemap();
            goalsData.Clear();
            initialGoalsData.Clear();
        }

        public GoalData[] GetTiles()
        {
            return goalsData.Values.ToArray();
        }

        private void SetTile(GoalData goalData)
        {
            var tile = tileLibrary.GetGoalTile(goalData.teamColor);
            var tilePosition = GetTilePosition(goalData.position);
            logisticTilemap.SetTile(tilePosition, tile);
        }

        public bool HasTile(Vector2Int position)
        {
            return goalsData.ContainsKey(position);
        }

        private void ClearTilemap()
        {
            foreach (var goalData in goalsData) {
                var tilePos = GetTilePosition(goalData.Key);
                logisticTilemap.SetTile(tilePos, null);
            }
        }

        private static Vector3Int GetTilePosition(Vector2Int position)
        {
            return new Vector3Int(position.x, position.y, GoalLayer);
        }
    }
}