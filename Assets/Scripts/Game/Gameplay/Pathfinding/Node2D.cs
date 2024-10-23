using Core;

namespace Game.Gameplay.Pathfinding
{
    public class Node2D
    {
        public bool Walkable;
        public int GridX;
        public int GridY;
        public int CostH;
        public int CostG;
        public Node2D Parent;
        public ConnectionDirection ConnectionDirection;

        public Node2D(bool walkable, int gridX, int gridY, ConnectionDirection connectionDirection = ConnectionDirection.None)
        {
            Walkable = walkable;
            GridX = gridX;
            GridY = gridY;
            ConnectionDirection = connectionDirection;
        }

        public int Cost => CostH + CostG;
    }
}