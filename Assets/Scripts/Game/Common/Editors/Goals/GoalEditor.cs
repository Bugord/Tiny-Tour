using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Core;
using Level;
using Level.Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Common.Editors.Goals
{
    public class GoalEditor : IGoalEditor
    {
        private readonly Tilemap logisticTilemap;
        private readonly ITileLibrary tileLibrary;

        private readonly Dictionary<Vector2Int, GoalData> goalsData;
        private GoalData[] cachedGoalData;

        private const int GoalLayer = 2;

        public GoalEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
        {
            this.tileLibrary = tileLibrary;
            logisticTilemap = tilemapsProvider.LogisticTilemap;

            goalsData = new Dictionary<Vector2Int, GoalData>();
        }

        public void SetTile(Vector2Int position, TeamColor teamColor)
        {
            var newGoalData = new GoalData(position, teamColor);
            SetTile(newGoalData);
        }

        public void SetTile(GoalData goalData)
        {
            if (goalsData.TryGetValue(goalData.position, out var existingGoalData)) {
                goalsData.Remove(existingGoalData.position);
            }
            
            var newGoalData = new GoalData(goalData.position, goalData.teamColor);
            goalsData[goalData.position] = goalData;
            SetTilemapTile(newGoalData);
        }

        public bool HasTile(Vector2Int position)
        {
            return goalsData.ContainsKey(position);
        }

        public void Load(GoalData[] tilesData)
        {
            cachedGoalData = tilesData;
            foreach (var cachedRoadTileData in tilesData) {
                SetTile(cachedRoadTileData);
            }
        }

        public GoalData[] GetTilesData()
        {
            return goalsData.Values.ToArray();
        }

        public void EraseTile(Vector2Int position)
        {
            goalsData.Remove(position);
            EraseTilemapTile(position);
        }

        public void Reset()
        {
            Clear();
            
            foreach (var initialCarGoalPoint in cachedGoalData) {
                SetTile(initialCarGoalPoint.position, initialCarGoalPoint.teamColor);
            }
        }

        public void Clear()
        {
            ClearTilemap();
            goalsData.Clear();
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

        private void SetTilemapTile(GoalData goalData)
        {
            var tile = tileLibrary.GetGoalTile(goalData.teamColor);
            logisticTilemap.SetTile(GetTilePosition(goalData.position), tile);
        }

        private void EraseTilemapTile(Vector2Int position)
        {
            logisticTilemap.SetTile(GetTilePosition(position), null);
        }
    }
}