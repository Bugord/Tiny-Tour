namespace Tiles
{
    public class RoadTileInfo
    {
        public ConnectionDirection ConnectionDirection { get; private set; }

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