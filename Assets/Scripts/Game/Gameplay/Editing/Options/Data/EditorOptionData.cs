using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_", menuName = "Data/Editor Option/Data")]
    public class EditorOptionData : ScriptableObject
    {
        [field: SerializeField]
        public string Id { get; private set; }
        
        [field: SerializeField]
        public Sprite DefaultIcon { get; private set; }
    }
}