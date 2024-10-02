using System;
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

    public void Setup(ToggleGroup toggleGroup, EditorOptionData editorOptionData)
    {
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