using System;
using System.Collections.Generic;
using System.Linq;
using Cars;
using Core;
using Level;
using Level.Data;
using Tiles.Editing.Logistic;
using Tiles.Editing.Options;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;
using Object = UnityEngine.Object;

namespace Tiles.Editing.Workshop
{
    public class WorkshopLogisticEditor : ITileEditor
    {
        private readonly Tilemap logisticTilemap;
        private readonly ITileLibrary tileLibrary;
        private readonly CarLibrary carLibrary;
        private readonly EditorSpawnPointView spawnPointViewPrefab;

        private readonly List<EditorSpawnPoint> spawnPoints;
        private readonly List<EditorTarget> targets;
        private readonly List<IntermediatePoint> intermediatePoints;

        private LogisticEditorOption selectedOption;
        private readonly Transform viewsRoot;

        public WorkshopLogisticEditor(Tilemap logisticTilemap, ITileLibrary tileLibrary,
            EditorSpawnPointView spawnPointViewPrefab, CarLibrary carLibrary, Transform viewsRoot)
        {
            this.logisticTilemap = logisticTilemap;
            this.tileLibrary = tileLibrary;
            this.spawnPointViewPrefab = spawnPointViewPrefab;
            this.carLibrary = carLibrary;
            this.viewsRoot = viewsRoot;

            targets = new List<EditorTarget>();
            spawnPoints = new List<EditorSpawnPoint>();
            intermediatePoints = new List<IntermediatePoint>();
        }

        public void OnTileDown(Vector3Int pos)
        {
            SetLogisticTile(pos);
        }

        public void OnTileUp()
        {
        }

        public void OnTileMove(Vector3Int pos)
        {
        }

        public void OnTileEraseDown(Vector3Int pos)
        {
            EraseLogisticTile(pos);
        }

        public void OnTileEraseMove(Vector3Int pos)
        {
            EraseLogisticTile(pos);
        }

        public List<BaseEditorOption> GetOptions()
        {
            var teams = EnumExtensions.GetAllEnums<Team>();
            var carTypes = EnumExtensions.GetAllEnums<CarType>();
            var options = new List<BaseEditorOption>();

            foreach (var team in teams) {
                foreach (var carType in carTypes) {
                    options.Add(new SpawnPointOption {
                        Team = team,
                        TileEditor = this,
                        CarType = carType,
                        Icon = carLibrary.GetCarData(carType).visualsData[team].directionSprites[Direction.Right]
                    });
                }
            }

            foreach (var team in teams) {
                options.Add(new TargetOption {
                    Team = team,
                    TileEditor = this,
                    Icon = tileLibrary.GetTargetTile(team).Sprite
                });
            }

            foreach (var team in teams) {
                options.Add(new IntermediatePointOption {
                    Team = team,
                    TileEditor = this,
                    Icon = tileLibrary.GetIntermediatePointTile(team).sprite
                });
            }

            return options;
        }

        public void SetOption(BaseEditorOption option)
        {
            selectedOption = (LogisticEditorOption)option;
        }

        public void Load(LogisticData logisticData)
        {
            spawnPoints.Clear();
            targets.Clear();
            intermediatePoints.Clear();
            logisticTilemap.ClearAllTiles();

            if (logisticData == null) {
                return;
            }

            if (logisticData.spawnPointsData != null) {
                foreach (var spawnPointData in logisticData.spawnPointsData) {
                    SetSpawnPoint(spawnPointData.team, spawnPointData.carType, spawnPointData.pos,
                        spawnPointData.direction);
                }
            }
            
            if (logisticData.targetsData != null) {
                foreach (var targetData in logisticData.targetsData) {
                    SetTargetTile(targetData.team, targetData.pos);
                }
            }

            if (logisticData.intermediatePointsData != null) {
                foreach (var intermediatePointData in logisticData.intermediatePointsData) {
                    SetIntermediatePointTile(intermediatePointData.team, intermediatePointData.pos);
                }
            }
        }

        public LogisticData Save()
        {
            var spawnPointsData = new List<SpawnPointData>();
            foreach (var spawnPoint in spawnPoints) {
                if (targets.All(target => target.Team != spawnPoint.Team)) {
                    continue;
                }

                spawnPointsData.Add(new SpawnPointData {
                    carType = spawnPoint.CarType,
                    direction = spawnPoint.Direction,
                    team = spawnPoint.Team,
                    pos = spawnPoint.CellPos,
                });
            }

            var targetsData = new List<TargetData>();
            foreach (var target in targets) {
                if (spawnPoints.All(spawnPoint => spawnPoint.Team != target.Team)) {
                    continue;
                }

                targetsData.Add(new TargetData {
                    team = target.Team,
                    pos = target.CellPos
                });
            }

            var intermediatePointsData = new List<IntermediatePointData>();
            foreach (var intermediatePoint in intermediatePoints) {
                if (spawnPointsData.All(spawnPoint => spawnPoint.team != intermediatePoint.Team)) {
                    continue;
                }

                intermediatePointsData.Add(new IntermediatePointData {
                    team = intermediatePoint.Team,
                    pos = intermediatePoint.Pos
                });
            }

            var logisticData = new LogisticData {
                spawnPointsData = spawnPointsData.ToArray(),
                targetsData = targetsData.ToArray(),
                intermediatePointsData = intermediatePointsData.ToArray()
            };

            return logisticData;
        }

        private void SetLogisticTile(Vector3Int pos)
        {
            switch (selectedOption) {
                case SpawnPointOption spawnPointOption:
                    SetSpawnPoint(spawnPointOption.Team, spawnPointOption.CarType, pos);
                    break;
                case TargetOption targetOption:
                    SetTargetTile(targetOption.Team, pos);
                    break;
                case IntermediatePointOption intermediatePointOption:
                    SetIntermediatePointTile(intermediatePointOption.Team, new Vector3Int(pos.x, pos.y, 1));
                    break;
            }
        }

        private void EraseLogisticTile(Vector3Int pos)
        {
            switch (selectedOption) {
                case SpawnPointOption:
                    EraseSpawnPoint(pos);
                    break;
                case TargetOption:
                    EraseTargetTile(pos);
                    break;
                case IntermediatePointOption:
                    EraseIntermediatePointTile(new Vector3Int(pos.x, pos.y, 1));
                    break;
            }
        }

        private void SetSpawnPoint(Team team, CarType carType, Vector3Int pos, Direction direction = Direction.Up)
        {
            var spawnPoint = spawnPoints.FirstOrDefault(point => point.CellPos == pos);

            if (spawnPoint != null) {
                if (spawnPoint.Team == team && spawnPoint.CarType == carType) {
                    spawnPoint.Direction = spawnPoint.Direction.GetNextValue();
                }
                else {
                    spawnPoint.Team = team;
                    spawnPoint.CarType = carType;
                }
            }
            else {
                var spawnPointView = Object.Instantiate(spawnPointViewPrefab, viewsRoot);
                spawnPoint = new EditorSpawnPoint(spawnPointView) {
                    Pos = logisticTilemap.CellToWorldCenter(pos),
                    CellPos = pos,
                    Direction = direction,
                    Team = team,
                    CarType = carType
                };

                spawnPoints.Add(spawnPoint);
            }

            spawnPoint.UpdateView();
        }

        private void EraseSpawnPoint(Vector3Int pos)
        {
            var spawnPoint = spawnPoints.FirstOrDefault(point => point.CellPos == pos);
            if (spawnPoint == null) {
                return;
            }

            Object.Destroy(spawnPoint.View.gameObject);
            spawnPoints.Remove(spawnPoint);
        }

        private void SetTargetTile(Team team, Vector3Int pos)
        {
            var target = targets.FirstOrDefault(t => t.CellPos == pos);
            if (target != null) {
                if (target.Team != team) {
                    targets.Remove(target);
                }
            }

            var sameTeamTarget = targets.FirstOrDefault(t => t.Team == team);
            if (sameTeamTarget != null) {
                logisticTilemap.SetTile(sameTeamTarget.CellPos, null);
                targets.Remove(sameTeamTarget);
            }

            logisticTilemap.SetTile(pos, tileLibrary.GetTargetTile(team));
            targets.Add(new EditorTarget {
                CellPos = pos,
                Team = team
            });
        }

        private void EraseTargetTile(Vector3Int pos)
        {
            var targetToErase = targets.FirstOrDefault(target => target.CellPos == pos);
            if (targetToErase == null) {
                return;
            }

            logisticTilemap.SetTile(pos, null);
            targets.Remove(targetToErase);
        }

        private void SetIntermediatePointTile(Team team, Vector3Int pos)
        {
            var intermediatePoint = intermediatePoints.FirstOrDefault(i => i.Pos == pos);
            if (intermediatePoint != null) {
                if (intermediatePoint.Team != team) {
                    intermediatePoints.Remove(intermediatePoint);
                }
            }

            var sameTeamPoint = intermediatePoints.FirstOrDefault(i => i.Team == team);
            if (sameTeamPoint != null) {
                logisticTilemap.SetTile(sameTeamPoint.Pos, null);
                intermediatePoints.Remove(sameTeamPoint);
            }

            logisticTilemap.SetTile(pos, tileLibrary.GetIntermediatePointTile(team));
            intermediatePoints.Add(new IntermediatePoint {
                Pos = pos,
                Team = team
            });
        }

        private void EraseIntermediatePointTile(Vector3Int pos)
        {
            var intermediatePoint = intermediatePoints.FirstOrDefault(target => target.Pos == pos);
            if (intermediatePoint == null) {
                return;
            }

            logisticTilemap.SetTile(pos, null);
            intermediatePoints.Remove(intermediatePoint);
        }
    }
}