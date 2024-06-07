using System.Collections.Generic;
using System.Linq;
using Core;
using Level;
using Level.Data;
using Tiles.Editing.Logistic;
using Tiles.Editing.Options;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Tiles.Editing
{
    public class InGameLogisticEditor : ITileEditor
    {
        private readonly Tilemap logisticTilemap;
        private readonly ITileLibrary tileLibrary;

        private readonly List<EditorTarget> targets;
        private readonly List<IntermediatePoint> intermediatePoints;

        private LogisticEditorOption selectedOption;

        public InGameLogisticEditor(Tilemap logisticTilemap, ITileLibrary tileLibrary)
        {
            this.logisticTilemap = logisticTilemap;
            this.tileLibrary = tileLibrary;
            targets = new List<EditorTarget>();
            intermediatePoints = new List<IntermediatePoint>();
        }

        public void OnTileDown(Vector3Int pos)
        {
        }

        public void OnTileUp()
        {
        }

        public void OnTileMove(Vector3Int pos)
        {
        }

        public void OnTileEraseDown(Vector3Int pos)
        {
        }

        public void OnTileEraseMove(Vector3Int pos)
        {
        }

        public List<BaseEditorOption> GetOptions()
        {
            return new List<BaseEditorOption>();
        }

        public void SetOption(BaseEditorOption option)
        {
        }

        public void Load(LogisticData logisticData)
        {
            logisticTilemap.ClearAllTiles();

            targets.Clear();
            intermediatePoints.Clear();

            if (logisticData == null) {
                return;
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