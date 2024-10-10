using System;
using UnityEngine;

namespace Core.UI.Controlls.Playing
{
    public class EditorOptionSettingsUI : MonoBehaviour
    {
        public event Action<TeamColor> ColorChanged; 
        
        [SerializeField]
        private OptionColorPicker colorPicker;
        
        public void SetupColors()
        {
            //colorPicker.gameObject.SetActive(true);
        }
    }
}