using Level;

namespace Common.Editors
{
    public interface IRoadService : IRoadLoader
    {
        public void Reset();
        RoadTileData[] SaveRoad();
    }
}