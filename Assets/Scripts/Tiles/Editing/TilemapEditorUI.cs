using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Tiles
{
    public class TilemapEditorUI : MonoBehaviour
    {
        public event Action<int> SelectedValueChanged; 

        [SerializeField]
        private TMP_Dropdown dropdown;

        private void Awake()
        {
            dropdown.onValueChanged.AddListener(OnSelectedValueChanged);
        }

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveAllListeners();
        }

        public void SetData(int count)
        {
            for (var i = 0; i < count; i++) {
                dropdown.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
            }
        }

        private void OnSelectedValueChanged(int value)
        {
            SelectedValueChanged?.Invoke(value);
        }
    }
}