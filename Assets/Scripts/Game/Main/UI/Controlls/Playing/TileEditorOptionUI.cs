using System;
using Core;
using Core.UI.Controlls.Playing;
using Gameplay.Editing.Options.Data;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TileEditorOptionUI : MonoBehaviour, IPointerClickHandler
{
    public event Action<string> ToggledOn;

    [SerializeField]
    private Toggle toggle;

    [SerializeField]
    private EditorOptionSettingsUI optionSettingsUI;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Image inactiveBackground;

    [SerializeField]
    private Image activeBackground;

    public string Id { get; private set; }

    private EditorOptionData editorOptionData;

    public void Setup(ToggleGroup toggleGroup, EditorOptionData editorOptionData)
    {
        this.editorOptionData = editorOptionData;
        toggle.group = toggleGroup;
        Id = editorOptionData.Id;

        if (editorOptionData.Icon) {
            icon.sprite = editorOptionData.Icon;
        }
        else {
            icon.enabled = false;
        }

        if (editorOptionData.CustomInactiveBackground) {
            inactiveBackground.sprite = editorOptionData.CustomInactiveBackground;
        }

        if (editorOptionData.CustomActiveBackground) {
            activeBackground.sprite = editorOptionData.CustomActiveBackground;
        }
    }

    public void UpdateColor(TeamColor teamColor)
    {
        if (!editorOptionData.ColoredIcons.TryGetValue(teamColor, out var iconSprite)) {
            return;
        }

        icon.sprite = iconSprite;
    }

    public void Toggle()
    {
        toggle.isOn = true;
    }

    public void OnToggleValueChanged()
    {
        if (toggle.isOn) {
            ToggledOn?.Invoke(Id);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right) {
            ToggleOptionSettingsUI();
        }
    }

    private void ToggleOptionSettingsUI()
    {
        if (editorOptionData.AlternativeOptions.Length > 0) {
            optionSettingsUI.gameObject.SetActive(!optionSettingsUI.gameObject.activeSelf);
        }
    }
}