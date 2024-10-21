using System.Linq;
using Common.Tilemaps;
using Game.Common.Editors.Road;
using Level;
using UnityEngine;
using Utility;

namespace Game.Gameplay.Editing.Editors
{
    public class PlayRoadEditor : BaseRoadEditor
    {
        public PlayRoadEditor(ITilemapsProvider tilemapsProvider, ITileLibrary tileLibrary) : base(tilemapsProvider, tileLibrary)
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
            foreach (var neighbourPosition in neighbourPositions) {
                if (!HasTile(neighbourPosition)) {
                    continue;
                }

                var neighbourRoadTile = RoadTilesData[neighbourPosition];
                var neighbourConnection = GridHelpers.GetPathDirection(neighbourPosition, position);

                var initialNeighbourRoadTile = CachedRoadTilesData.FirstOrDefault(data => data.position == neighbourPosition);
                if (initialNeighbourRoadTile != null && initialNeighbourRoadTile.HasDirection(neighbourConnection)) {
                    continue;
                }
                
                neighbourRoadTile.TurnOffDirection(neighbourConnection);
                SetTilemapTile(neighbourRoadTile);
            }
            
            var initialRoadData = CachedRoadTilesData.FirstOrDefault(data => data.position == position);
            if (initialRoadData != null) {
                SetTile(initialRoadData);
            }
        }
    }
}