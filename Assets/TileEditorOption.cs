using System;
using UnityEngine;
using UnityEngine.UI;

public class TileEditorOption : MonoBehaviour
{
    [SerializeField]
    private Toggle toggle;

    [SerializeField]
    private Image image;

    private int index;
    private Action<int> onToggleOn;

    public void Setup(Sprite sprite, ToggleGroup toggleGroup, int index, bool isOn, Action<int> onToggleOn)
    {
        toggle.group = toggleGroup;
        toggle.isOn = isOn;
        image.sprite = sprite;
        this.onToggleOn = onToggleOn;
        this.index = index;
    }

    public void OnToggleValueChanged()
    {
        if (toggle.isOn) {
            onToggleOn?.Invoke(index);
        }
    }
}
