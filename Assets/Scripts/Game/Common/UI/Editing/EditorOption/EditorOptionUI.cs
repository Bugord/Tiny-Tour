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
        public event Action<string> ToggledOn;
        public event Action<TeamColor> ColorSelected;
        public event Action<int> AlternativeSelected;

        [field: SerializeField]
        public EditorOptionsConfigurationUI EditorOptionsConfigurationUI { get; private set; }

        [SerializeField]
        protected Image activeBorderImage;

        [SerializeField]
        protected Image inactiveBorderImage;

        private Toggle toggle;
        protected Image Icon;

        public string Id { get; protected set; }

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            Icon = toggle.image;
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);

            EditorOptionsConfigurationUI.EditorOptionColorPicker.ColorSelected += ColorSelected;
            EditorOptionsConfigurationUI.EditorOptionAlternativePicker.AlternativeSelected += AlternativeSelected;
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveAllListeners();

            EditorOptionsConfigurationUI.EditorOptionColorPicker.ColorSelected -= ColorSelected;
            EditorOptionsConfigurationUI.EditorOptionAlternativePicker.AlternativeSelected -= AlternativeSelected;
        }

        public void SetId(string id)
        {
            Id = id;
        }

        public void SetToggleGroup(ToggleGroup toggleGroup)
        {
            toggle.group = toggleGroup;
        }

        public void EnableColorPicker()
        {
        }

        public void SetAlternatives(Dictionary<int, Sprite> alternatives)
        {
            EditorOptionsConfigurationUI.EditorOptionAlternativePicker.SetData(alternatives);
        }

        public void SetVisuals(Sprite icon, Sprite activeBorderSprite = null, Sprite inactiveBorderSprite = null)
        {
            Icon.sprite = icon;
            
            if (activeBorderSprite) {
                activeBorderImage.sprite = activeBorderSprite;
            }

            if (inactiveBorderSprite) {
                inactiveBorderImage.sprite = inactiveBorderSprite;
            }
        }

        public void Toggle()
        {
            toggle.isOn = true;
        }

        private void OnToggleValueChanged(bool isOn)
        {
            if (isOn) {
                ToggledOn?.Invoke(Id);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right) {
                EditorOptionsConfigurationUI.gameObject.SetActive(!EditorOptionsConfigurationUI.gameObject.activeSelf);
            }
        }
    }
}