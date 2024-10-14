using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Common.UI.Editing.EditorOption.ColorPicker
{
    [RequireComponent(typeof(Toggle))]
    public class ColorPickerToggle : MonoBehaviour
    {
        public event Action<TeamColor> ColorSelected;

        [SerializeField]
        private TeamColor color;
        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnToggleChanged);
        }

        private void OnToggleChanged(bool isOn)
        {
            if (isOn) {
                ColorSelected?.Invoke(color);
            }
        }
    }
}