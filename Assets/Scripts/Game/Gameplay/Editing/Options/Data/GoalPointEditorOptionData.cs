using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_goal_point", menuName = "Data/Editor Option/Goal Point Data")]
    public class GoalPointEditorOptionData : EditorOptionData
    {
        [field: SerializeField]
        public SerializedDictionary<TeamColor, Sprite> ColoredGoalPointsIcons { get; private set; }
    }
}