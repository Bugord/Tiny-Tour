using System;
using Core;
using UnityEngine;

namespace Game.Main.UI.Controls.Playing
{
    public class OptionColorPicker : MonoBehaviour
    {
        public event Action<TeamColor> ColorPicked; 
        
        [SerializeField]
        private ColorButton[] colorButtons;

        private void OnEnable()
        {
            foreach (var colorButton in colorButtons) {
                colorButton.Clicked += ColorPicked;
            }
        }

        private void OnDisable()
        {
            foreach (var colorButton in colorButtons) {
                colorButton.Clicked -= ColorPicked;
            }
        }
    }
}