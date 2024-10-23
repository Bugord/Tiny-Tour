using System.Collections.Generic;
using System.Linq;
using Common.Tilemaps;
using UnityEngine;

namespace Game.Gameplay.Utility
{
    public class RoadTilemapGridConverter : IRoadTilemapGridConverter
    {
        private readonly ITilemapsProvider tilemapsProvider;
        private Vector2Int Offset => (Vector2Int)tilemapsProvider.RoadTilemap.cellBounds.min;
        
        public RoadTilemapGridConverter(ITilemapsProvider tilemapsProvider)
        {
            this.tilemapsProvider = tilemapsProvider;
        }
        
        public Vector2Int TilemapToGrid(Vector2Int tilemapPos)
        {
            return tilemapPos - Offset;
        }    
        
        public Vector2Int GridToTilemap(Vector2Int gridPosition)
        {
            return gridPosition + Offset;
        }  
        
        public Vector2Int[] TilemapToGrid(IEnumerable<Vector2Int> tilemapPositions)
        {
            return tilemapPositions.Select(pos => pos - Offset).ToArray();
        }    
        
        public Vector2Int[] GridToTilemap(IEnumerable<Vector2Int> gridPositions)
        {
            return gridPositions.Select(pos => pos + Offset).ToArray();
        }
    }
}