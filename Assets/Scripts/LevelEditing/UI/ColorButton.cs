using System;
using AYellowpaper.SerializedCollections;
using Core;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace LevelEditing.UI
{
    [RequireComponent(typeof(Button))]
    public class ColorButton : MonoBehaviour
    {
        public event Action<TeamColor> ColorChanged;
        
        [SerializedDictionary]
        public SerializedDictionary<TeamColor, ButtonSprites> coloredSpritesVariant;

        private Button button;
        private TeamColor currentColor;

        public TeamColor Color => currentColor;
        
        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnButtonClick);

            UpdateSprites();
        }

        private void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        private void OnButtonClick()
        {
            currentColor = currentColor.GetNextValue();
            UpdateSprites();
            
            ColorChanged?.Invoke(currentColor);
        }

        private void UpdateSprites()
        {
            if (coloredSpritesVariant.TryGetValue(currentColor, out var buttonSprites)) {
                button.image.sprite = buttonSprites.defaultButtonSprite;
                button.spriteState = buttonSprites.spriteState;
            }
            else {
                Debug.LogWarning($"Button has no configuration for color {currentColor}");
            }
        }

        [Serializable]
        public class ButtonSprites
        {
            public Sprite defaultButtonSprite;
            public SpriteState spriteState;
        }
    }
}