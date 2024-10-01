using System;
using UnityEngine;
using UnityEngine.UI;

namespace Common.UI
{
    [RequireComponent(typeof(Toggle))]
    public class SwitchableToggle : MonoBehaviour
    {
        public event Action<bool> ValueChanged;

        [Header("Off State")]
        [SerializeField]
        private Sprite offSprite;

        [SerializeField]
        private SpriteState offSpriteState;

        private Toggle toggle;
        private Sprite onSprite;
        private SpriteState onSpriteState;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();

            onSprite = toggle.image.sprite;
            onSpriteState = toggle.spriteState;
            toggle.onValueChanged.AddListener(OnToggleChanged);

            if (!toggle.isOn) {
                toggle.image.sprite = offSprite;
            }
        }

        public void SetIsOnWithoutNotify(bool isOn)
        {
            toggle.SetIsOnWithoutNotify(isOn);
            UpdateSprite();
        }

        private void UpdateSprite()
        {
            toggle.spriteState = toggle.isOn ? onSpriteState : offSpriteState;
            toggle.image.sprite = toggle.isOn ? onSprite : offSprite;
        }

        private void OnToggleChanged(bool isOn)
        {
            UpdateSprite();
            ValueChanged?.Invoke(isOn);
        }
    }
}