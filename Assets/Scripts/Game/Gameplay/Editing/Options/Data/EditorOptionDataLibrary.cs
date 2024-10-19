using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "library_editor_option_data", menuName = "Data/Editor Option/Library")]
    public class EditorOptionDataLibrary : ScriptableObject
    {
        [field: SerializeField]
        public EditorOptionData RoadEditorOptionData { get; private set; }
        
        [field: SerializeField]
        public EraseEditorOptionData EraseEditorOptionData { get; private set; }        
        
        [field: SerializeField]
        public TerrainEditorOptionData TerrainEditorOptionData { get; private set; }   
        
        [field: SerializeField]
        public CarSpawnPointEditorOptionData CarSpawnPointEditorOptionData { get; private set; }        
        
        [field: SerializeField]
        public GoalPointEditorOptionData GoalPointEditorOptionData { get; private set; }
    }
}