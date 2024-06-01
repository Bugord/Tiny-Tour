namespace Tiles
{
    public class RoadTileInfo
    {
        private readonly ConnectionDirection initialConnectionDirection;
        
        public ConnectionDirection ConnectionDirection;
        public bool WasInitiallyPlaced;

        public RoadTileInfo(ConnectionDirection connectionDirection = ConnectionDirection.None, bool wasInitiallyPlaced = false)
        {
            initialConnectionDirection = connectionDirection;
            ConnectionDirection = connectionDirection;
            WasInitiallyPlaced = wasInitiallyPlaced;
        }

        public void ResetConnectionDirection()
        {
            ConnectionDirection = initialConnectionDirection;
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