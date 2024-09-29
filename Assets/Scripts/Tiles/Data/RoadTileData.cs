using System;
using Core;
using Tiles;

namespace Level
{
    [Serializable]
    public class RoadTileData : BaseTileData
    {
        public ConnectionDirection connectionDirection;
        
        public void TurnOnDirection(ConnectionDirection direction)
        {
            connectionDirection |= direction;
        }

        public void TurnOffDirection(ConnectionDirection direction)
        {
            connectionDirection &= ~direction;
        }
    }
}