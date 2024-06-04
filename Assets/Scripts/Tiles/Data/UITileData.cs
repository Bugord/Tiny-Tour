using System;
using Core;
using Tiles;

namespace Level
{
    [Serializable]
    public class UITileData : BaseTileData
    {
        public UITileType tileType;
        public Team team;
    }
}