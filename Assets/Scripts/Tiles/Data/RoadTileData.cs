using System;
using Core;
using Tiles;

namespace Level
{
    [Serializable]
    public class RoadTileData : BaseTileData
    {
        public ConnectionDirection connectionDirection;
    }
}