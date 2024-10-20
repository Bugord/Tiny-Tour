using System;
using AYellowpaper.SerializedCollections;
using Core;
using Game.Common.Obstacles;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_obstacle", menuName = "Data/Editor Option/Obstacle Data")]
    public class ObstacleEditorOptionData : EditorOptionData
    {
        [field: SerializeField]
        public SerializedDictionary<ObstacleType, ObstacleVariants> ObstacleSprites { get; private set; }

        [Serializable]
        public class ObstacleVariants
        {
            public Sprite defaultSprite;
            public SerializedDictionary<TeamColor, Sprite> coloredObstacleVariants;

            public Sprite GetColoredObstacleVariant(TeamColor teamColor)
            {
                return coloredObstacleVariants.ContainsKey(teamColor)
                    ? coloredObstacleVariants[teamColor]
                    : defaultSprite;
            }
        }
    }
}