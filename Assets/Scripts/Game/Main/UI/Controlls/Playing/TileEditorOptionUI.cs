using System;
using Core;
using Gameplay.Editing.Options.Data;
using UnityEngine;
using UnityEngine.UI;

public class TileEditorOptionUI : MonoBehaviour
{
    public event Action<string> ToggledOn;

    [SerializeField]
    private Toggle toggle;

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
        if (editorOptionData is not ColoredEditorOptionData coloredEditorOptionData) {
            return;
        }
        
        if (coloredEditorOptionData.ColoredIcons.TryGetValue(teamColor, out var iconSprite)) {
            icon.sprite = iconSprite;
        }
        else {
            Debug.LogWarning($"Color {teamColor} is not configured for editor option {Id}");
        }
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
}