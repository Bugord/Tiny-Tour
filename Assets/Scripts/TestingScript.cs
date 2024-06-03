using Cars;
using Pathfinding;
using UnityEngine;

namespace DefaultNamespace
{
    public class TestingScript : MonoBehaviour
    {
        [SerializeField]
        private Car car;

        [SerializeField]
        private PathfindingController pathfindingController;

        [ContextMenu("Test")]
        public void Test()
        {
            var path = pathfindingController.FindPathWorld(pathfindingController.from, pathfindingController.to);
            car.PlayPath(path);
        }
    }
}