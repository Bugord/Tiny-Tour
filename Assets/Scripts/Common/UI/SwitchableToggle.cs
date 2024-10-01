using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    public class SwitchableToggle : MonoBehaviour
    {
        [SerializeField]
        private Toggle toggle;
        
        [Header("Off State")]
        [SerializeField]
        private Sprite offSprite;
        
        [SerializeField]
        private SpriteState offSpriteState;
        
        private Sprite onSprite;
        private SpriteState onSpriteState;

        private void Awake()
        {
            onSprite = toggle.image.sprite;
            onSpriteState = toggle.spriteState;
            toggle.onValueChanged.AddListener(OnToggleChanged);

            if (!toggle.isOn) {
                toggle.image.sprite = offSprite;
            }
        }

        private void OnToggleChanged(bool isOn)
        {
            toggle.spriteState = isOn ? onSpriteState : offSpriteState;
            toggle.image.sprite = isOn ? onSprite : offSprite;
        }
    }
}