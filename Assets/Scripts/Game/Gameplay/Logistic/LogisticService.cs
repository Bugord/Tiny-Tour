﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using Core;
using Core.Logging;
using Game.Common.Editors.Goals;
using Game.Common.Editors.Road;
using Game.Common.Level.Data;
using Game.Gameplay.Pathfinding;
using UnityEngine;

namespace Game.Gameplay.Logistic
{
    public class LogisticService : ILogisticService
    {
        private readonly ILogger<LogisticService> logger;
        private readonly IGoalLevelEditor goalLevelEditor;
        private readonly IRoadEditor roadEditor;
        private readonly IPathfindingService pathfindingService;
        private readonly ITilemapPositionConverter tilemapPositionConverter;
        private readonly Dictionary<TeamColor, Vector2Int> goals;

        public LogisticService(ILogger<LogisticService> logger, IGoalLevelEditor goalLevelEditor, IPathfindingService pathfindingService, ITilemapPositionConverter tilemapPositionConverter)
        {
            this.logger = logger;
            this.goalLevelEditor = goalLevelEditor;
            this.pathfindingService = pathfindingService;
            this.tilemapPositionConverter = tilemapPositionConverter;
            goals = new Dictionary<TeamColor, Vector2Int>();
        }

        public void LoadLogistic(LogisticData logisticData)
        {
            if (logisticData == null) {
                logger.LogError("Logistic data is null");
                return;
            }

            if (logisticData.roadTileData == null) {
                logger.LogError("Road tiles are null");
                return;
            }

            foreach (var targetData in logisticData.goalsData) {
                AddGoal(targetData.position, targetData.teamColor);
            }
        }

        public void AddGoal(Vector2Int position, TeamColor teamColor)
        {
            if (goals.ContainsKey(teamColor)) {
                logger.LogError($"Goal for {teamColor} was already added");
                return;
            }          
            
            goals.Add(teamColor, position);
            goalLevelEditor.SetTile(position, teamColor);
        }

        public Vector2[] GetPathForCar(Vector2 carPos, TeamColor carTeamColor)
        {
            if (!goals.ContainsKey(carTeamColor)) {
                logger.LogWarning($"Goal for car {carTeamColor} is not setted");
                return Array.Empty<Vector2>();
            }

            var goalPosition = goals[carTeamColor];
            var carTilePos = tilemapPositionConverter.WorldToCell(carPos);
            var path = pathfindingService.FindPath(carTilePos, goalPosition);

            var worldPath = path.Select(point => tilemapPositionConverter.CellToWorld(point)).ToArray();
            
            return worldPath;
        }

        public void Clear()
        {
            goalLevelEditor.Clear();
            goals.Clear();
        }
    }
}