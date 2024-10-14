using System;
using AYellowpaper.SerializedCollections;
using Core;
using Core.UI;
using UnityEngine;
using Utility;

namespace Game.Workshop.UI
{
    public class ColorButton : Button
    {
        public event Action<TeamColor> ColorChanged;

        [SerializedDictionary]
        public SerializedDictionary<TeamColor, ButtonSprites> coloredSpritesVariant;

        public TeamColor Color { get; private set; }

        private void Awake()
        {
            UpdateSprites();
        }

        protected override void OnLeftMouseClicked()
        {
            Color = Color.GetNextValue();
            UpdateSprites();
            ColorChanged?.Invoke(Color);
        }

        protected override void OnRightMouseClicked()
        {
            Color = Color.GetPreviousValue();
            UpdateSprites();
            ColorChanged?.Invoke(Color);
        }

        private void UpdateSprites()
        {
            if (coloredSpritesVariant.TryGetValue(Color, out var buttonSprites)) {
                ButtonSprites = buttonSprites;
            }
            else {
                Debug.LogWarning($"Button has no configuration for color {Color}");
            }
        }
    }
}