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
        private Vector3[] waypoints;
        
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

        public void PlayPath(Vector3[] waypoints)
        {
            this.waypoints = waypoints;
            transform.position = waypoints[0];

            var path = new Path(PathType.Linear, waypoints, 0, Color.red);
            pathTween = transform
                .DOPath(path, waypoints.Length / carData.speed, PathMode.TopDown2D)
                .OnWaypointChange(ChangeCarDirection)
                .OnComplete(Finish)
                .SetEase(Ease.Linear);
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
            if (waypointIndex >= waypoints.Length - 1) {
                return;
            }

            var direction = Directions.GetDirection(waypoints[waypointIndex], waypoints[waypointIndex + 1]);
            UpdateSprite(direction);
        }

        private void CrashCar()
        {
            IsCrashed = true;
            pathTween.Kill();
            transform.DORotate(new Vector3(0, 0, Random.Range(0f, 360f)), 0.5f);

            Crashed?.Invoke();
        }
    }
}