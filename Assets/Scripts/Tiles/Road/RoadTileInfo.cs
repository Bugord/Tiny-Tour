using Core;

namespace Tiles
{
    public class RoadTileInfo
    {
        public ConnectionDirection ConnectionDirection;
        public readonly bool WasInitiallyPlaced;

        public RoadTileInfo(ConnectionDirection connectionDirection = ConnectionDirection.None, bool wasInitiallyPlaced = false)
        {
            ConnectionDirection = connectionDirection;
            WasInitiallyPlaced = wasInitiallyPlaced;
        }

        public void TurnOnDirection(ConnectionDirection direction)
        {
            ConnectionDirection |= direction;
        }

        public void TurnOffDirection(ConnectionDirection direction)
        {
            ConnectionDirection &= ~direction;
        }
    }
}