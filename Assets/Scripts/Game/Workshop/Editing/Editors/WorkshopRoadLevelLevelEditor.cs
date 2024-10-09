using System.Linq;
using Common.Tilemaps;
using Game.Common.Editors.Road;
using Level;
using UnityEngine;
using Utility;

namespace Game.Workshop.Editing.Editors
{
    public class WorkshopRoadLevelLevelEditor : BaseRoadLevelLevelEditor
    {
        public WorkshopRoadLevelLevelEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary)
            : base(tilemapsProvider, tileLibrary)
        {
        }

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
    }
}