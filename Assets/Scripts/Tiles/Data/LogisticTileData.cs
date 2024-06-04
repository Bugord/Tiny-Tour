using System;
using Core;
using Tiles;

namespace Level
{
    [Serializable]
    public class LogisticTileData : BaseTileData
    {
        public LogisticTileType tileType;
        public Team team;
    }
}