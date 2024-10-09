using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using UnityEngine;
using Utility;
using Random = UnityEngine.Random;

namespace Cars
{
    public class Car : MonoBehaviour
    {
        public event Action Crashed;
        public event Action Finished;

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private CarData carData;

        private Tween pathTween;
        private Tween crashAnimationTween;
        private Vector3[] worldWaypoints;

        public Vector2 Position => transform.position;
        public bool IsCrashed { get; private set; }
        public bool IsFinished { get; private set; }

        public TeamColor TeamColor { get; private set; }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            if (IsFinished) {
                return;
            }

            if (!collider2D.TryGetComponent<Car>(out var car)) {
                return;
            }

            if (!car.IsFinished) {
                CrashCar();
            }
        }

        public void PlayPath(Vector2[] waypoints)
        {
            if (!waypoints.Any()) {
                return;
            }
            
            worldWaypoints = waypoints.Select(waypoint => (Vector3)waypoint).ToArray();
            transform.position = worldWaypoints[0];

            var path = new Path(PathType.Linear, worldWaypoints, 0, Color.red);
            pathTween = transform
                .DOPath(path, worldWaypoints.Length / carData.speed, PathMode.TopDown2D)
                .OnWaypointChange(ChangeCarDirection)
                .OnComplete(Finish)
                .SetEase(Ease.Linear);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetData(CarData carData)
        {
            this.carData = carData;
            UpdateSprite();
        }

        public void SetDirection(Direction direction)
        {
            UpdateSprite(direction);
        }

        public void SetColor(TeamColor teamColor)
        {
            TeamColor = teamColor;
        }

        public void Reset()
        {
            IsCrashed = false;
            IsFinished = false;
            transform.rotation = Quaternion.identity;

            pathTween?.Kill();
            crashAnimationTween?.Kill();
        }

        private void UpdateSprite(Direction direction = Direction.Down)
        {
            spriteRenderer.sprite = carData.visualsData[TeamColor].directionSprites[direction];
        }

        private void Finish()
        {
            IsFinished = true;
            Finished?.Invoke();
        }

        private void ChangeCarDirection(int waypointIndex)
        {
            if (waypointIndex >= worldWaypoints.Length - 1) {
                return;
            }

            var direction = Directions.GetDirection(worldWaypoints[waypointIndex], worldWaypoints[waypointIndex + 1]);
            UpdateSprite(direction);
        }

        private void CrashCar()
        {
            IsCrashed = true;
            pathTween.Kill();
            crashAnimationTween = transform.DORotate(new Vector3(0, 0, Random.Range(0f, 360f)), 0.5f);

            Crashed?.Invoke();
        }
    }
}