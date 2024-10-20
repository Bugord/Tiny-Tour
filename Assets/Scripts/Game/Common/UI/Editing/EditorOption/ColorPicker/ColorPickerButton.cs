using System;
using Core;
using Core.UI;
using UnityEngine;

namespace Game.Common.UI.Editing.EditorOption.ColorPicker
{
    public class ColorPickerButton : Button
    {
        public event Action<TeamColor> ColorSelected;

        [SerializeField]
        private TeamColor color;

        protected override void OnLeftMouseClicked()
        {
            ColorSelected?.Invoke(color);
        }
    }
}