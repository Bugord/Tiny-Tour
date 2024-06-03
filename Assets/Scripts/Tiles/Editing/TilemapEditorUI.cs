using System;
using System.Collections.Generic;
using Tiles.Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Tiles
{
    public class TilemapEditorUI : MonoBehaviour
    {
        public event Action<BaseEditorOption> SelectedValueChanged;

        private List<BaseEditorOption> options;

        [SerializeField]
        private ToggleGroup toggleGroup;

        [SerializeField]
        private TileEditorOption tileEditorOptionPrefab;

        [SerializeField]
        private Transform optionsRoot;
        
        public void SetData(List<BaseEditorOption> editorOptions)
        {
            options = editorOptions;
            for (var i = 0; i < editorOptions.Count; i++) {
                var editorOption = editorOptions[i];
                var option = Instantiate(tileEditorOptionPrefab, optionsRoot);
                option.Setup(editorOption.Icon, toggleGroup, i, i == 0, OnToggleOn);
            }
        }

        public void OnToggleOn(int index)
        {
            SelectedValueChanged?.Invoke(options[index]);
        }
    }
}