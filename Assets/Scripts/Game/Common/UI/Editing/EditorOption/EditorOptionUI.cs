using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Game.Common.UI.Editing.EditorOption
{
    [RequireComponent(typeof(Toggle))]
    public class EditorOptionUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action LeftClicked;
        public event Action RightClicked;

        [field: SerializeField]
        public EditorOptionsConfigurationUI EditorOptionsConfigurationUI { get; private set; }

        [SerializeField]
        protected Image activeBorderImage;

        [SerializeField]
        protected Image inactiveBorderImage;

        private Toggle toggle;
        protected Image Icon;
        
        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            Icon = toggle.image;
        }

        public void SetIcon(Sprite icon)
        {
            Icon.sprite = icon;
        }

        public void SetBorders(Sprite activeBorderSprite, Sprite inactiveBorderSprite)
        {
            activeBorderImage.sprite = activeBorderSprite;
            inactiveBorderImage.sprite = inactiveBorderSprite;
        }

        public void SetToggled(bool isOn)
        {
            toggle.isOn = isOn;
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            toggle.group = toggleGroup;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button) {
                case PointerEventData.InputButton.Left:
                    LeftClicked?.Invoke();
                    break;
                case PointerEventData.InputButton.Right:
                    RightClicked?.Invoke();
                    break;
            }
        }
    }
}