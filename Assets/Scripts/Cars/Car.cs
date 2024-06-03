using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using Utility;

namespace Cars
{
    public class Car : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private CarData carData;

        private Tween pathTween;
        private Vector3[] waypoints;

        public void PlayPath(Vector3[] waypoints)
        {
            this.waypoints = waypoints;
            transform.position = waypoints[0];
            
            var path = new Path(PathType.Linear, waypoints, 0, Color.red);
            pathTween = transform
                .DOPath(path, waypoints.Length / carData.speed, PathMode.TopDown2D)
                .OnWaypointChange(ChangeCarDirection)
                .SetEase(Ease.Linear);
        }

        private void ChangeCarDirection(int waypointIndex)
        {
            if (waypointIndex >= waypoints.Length) {
                return;
            }
            
            var direction = Directions.GetDirection(waypoints[waypointIndex], waypoints[waypointIndex + 1]);
            spriteRenderer.sprite = carData.directionSprites[direction];
        }
    }
}