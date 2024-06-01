using System;
using Tiles;

namespace Level
{
    [Serializable]
    public class RoadTileData : BaseTileData
    {
        public ConnectionDirection connectionDirection;
    }
}