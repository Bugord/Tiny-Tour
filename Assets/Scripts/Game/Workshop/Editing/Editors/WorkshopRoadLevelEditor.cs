using System.Collections.Generic;
using System.Linq;
using Common.Editors.Road;
using Common.Tilemaps;
using Core;
using Game.Common.Editors.Road;
using Level;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utility;

namespace Game.Workshop.LevelEditor.Editors
{
    public class WorkshopRoadLevelEditor : BaseRoadLevelEditor
    {
        public override void EraseTile(Vector2Int position)
        {
            if (!HasTile(position)) {
                return;
            }

            RoadTilesData.Remove(position);
            EraseTilemapTile(position);

            var neighbourPositions = GridHelpers.GetNeighborPos(position);
            var neighbourRoadsData = RoadTilesData.Values.Where(data => neighbourPositions.Contains(data.position));

            foreach (var neighbourRoadData in neighbourRoadsData) {
                var neighbourConnection = GridHelpers.GetPathDirection(neighbourRoadData.position, position);
                neighbourRoadData.TurnOffDirection(neighbourConnection);

                SetTilemapTile(neighbourRoadData);
            }
        }

        public WorkshopRoadLevelEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary) : base(
            tilemapsProvider, tileLibrary)
        {
        }
    }
}