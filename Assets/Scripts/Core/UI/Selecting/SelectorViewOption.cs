using System;
using UnityEngine;

namespace Core.UI.Selecting
{
    public class SelectorViewOption : MonoBehaviour
    {
        public event Action<int> OptionPressed;

        private Button.Button button;

        public int Id { get; set; }

        private void Awake()
        {
            button = GetComponent<Button.Button>();
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
            OptionPressed?.Invoke(Id);
        }
    }
}