using System.Collections.Generic;
using System.Linq;
using Core;
using Level;
using Level.Data;
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

        private LogisticEditorOption selectedOption;

        public InGameLogisticEditor(Tilemap logisticTilemap, ITileLibrary tileLibrary)
        {
            this.logisticTilemap = logisticTilemap;
            this.tileLibrary = tileLibrary;
            targets = new List<EditorTarget>();
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
            if (logisticData == null) {
                return;
            }
            
            foreach (var targetData in logisticData.targetsData) {
                SetTargetTile(targetData.team, targetData.pos);
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
    }
}