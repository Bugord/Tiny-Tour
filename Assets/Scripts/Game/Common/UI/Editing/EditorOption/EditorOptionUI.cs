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

        private Dictionary<int, Sprite> alternatives;

        public string Id { get; protected set; }

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
            Icon = toggle.image;
        }

        private void OnEnable()
        {
            toggle.onValueChanged.AddListener(OnToggleValueChanged);

            EditorOptionsConfigurationUI.EditorOptionColorPicker.ColorSelected += OnColorSelected;
            EditorOptionsConfigurationUI.EditorOptionAlternativePicker.AlternativeSelected += OnAlternativeSelected;
        }

        private void OnDisable()
        {
            toggle.onValueChanged.RemoveAllListeners();

            EditorOptionsConfigurationUI.EditorOptionColorPicker.ColorSelected -= OnColorSelected;
            EditorOptionsConfigurationUI.EditorOptionAlternativePicker.AlternativeSelected -= OnAlternativeSelected;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right) {
                if (!toggle.isOn) {
                    toggle.group.SetAllTogglesOff();
                    
                    toggle.isOn = true;
                    toggle.group.NotifyToggleOn(toggle);
                }

                if (!EditorOptionsConfigurationUI.IsConfigured) {
                    return;
                }
                if (EditorOptionsConfigurationUI.gameObject.activeSelf) {
                    EditorOptionsConfigurationUI.Close();
                }
                else {
                    EditorOptionsConfigurationUI.Open();
                }
            }
        }

        public void Init(string id, ToggleGroup toggleGroup)
        {
            Id = id;
            toggle.group = toggleGroup;
        }

        public void EnableColorPicker()
        {
            EditorOptionsConfigurationUI.EnableColorPicker();
        }

        public void SetAlternatives(Dictionary<int, Sprite> alternatives)
        {
            this.alternatives = alternatives;
            EditorOptionsConfigurationUI.SetAlternatives(alternatives);
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
            else {
                EditorOptionsConfigurationUI.Close();
            }
        }

        private void OnColorSelected(TeamColor color)
        {
            ColorSelected?.Invoke(color);
            EditorOptionsConfigurationUI.Close();
        }

        private void OnAlternativeSelected(int alternativeId)
        {
            var icon = alternatives[alternativeId];
            SetVisuals(icon);

            EditorOptionsConfigurationUI.Close();

            AlternativeSelected?.Invoke(alternativeId);
        }
    }
}