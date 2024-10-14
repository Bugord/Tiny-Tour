using UnityEngine;

namespace Game.Gameplay.Editing.Options.Data
{
    [CreateAssetMenu(fileName = "data_editor_erase", menuName = "Data/Editor Option/Erase Data")]
    public class EraseEditorOptionData : EditorOptionData
    {
        [field: SerializeField]
        public Sprite ActiveBorderSprite { get; private set; }
        
        [field: SerializeField]
        public Sprite InactiveBorderSprite { get; private set; }
    }
}