using System;
using UnityEngine;

namespace Core.UI.Controlls.Playing
{
    [RequireComponent(typeof(Button))]
    public class ColorButton : MonoBehaviour
    {
        public event Action<TeamColor> Clicked;
        
        [field: SerializeField]
        public TeamColor Color { get; private set; }

        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            button.ClickedLeft += OnClickedLeft;
        }

        private void OnDisable()
        {
            button.ClickedLeft -= OnClickedLeft;
        }

        private void OnClickedLeft()
        {
            Clicked?.Invoke(Color);
        }
    }
}