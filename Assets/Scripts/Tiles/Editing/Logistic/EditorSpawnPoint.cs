using Cars;
using Core;
using UnityEngine;

namespace Tiles.Editing
{
    public class EditorSpawnPoint
    {
        public Vector3Int CellPos;
        public Vector3 Pos;
        public Team Team;
        public Direction Direction;
        public CarType CarType;
        public readonly EditorSpawnPointView View;

        public EditorSpawnPoint(EditorSpawnPointView view)
        {
            View = view;
        }

        public void UpdateView()
        {
            View.transform.position = Pos;
            View.SetVisual(CarType, Team, Direction);
        }
    }
}