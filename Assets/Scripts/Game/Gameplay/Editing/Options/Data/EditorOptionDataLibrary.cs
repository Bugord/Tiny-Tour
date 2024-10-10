using UnityEngine;

namespace Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "library_editor_option_data", menuName = "Data/Editor Option/Library")]
    public class EditorOptionDataLibrary : ScriptableObject
    {
        [field: SerializeField]
        public EditorOptionData RoadEditorOptionData { get; private set; }
        
        [field: SerializeField]
        public EditorOptionData EraseEditorOptionData { get; private set; }        
        
        [field: SerializeField]
        public EditorOptionData GroundEditorOptionData { get; private set; }
        
        [field: SerializeField]
        public EditorOptionData WaterEditorOptionData { get; private set; }
        
        [field: SerializeField]
        public EditorOptionData BridgeEditorOptionData { get; private set; }        
        
        [field: SerializeField]
        public EditorOptionData CarSpawnPointEditorOptionData { get; private set; }        
        
        [field: SerializeField]
        public EditorOptionData GoalPointEditorOptionData { get; private set; }
    }
}