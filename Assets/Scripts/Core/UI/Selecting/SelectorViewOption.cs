﻿using System;
using UnityEngine;

namespace Core.UI
{
    public class SelectorViewOption : MonoBehaviour
    {
        public event Action<int> OptionPressed;

        private Button button;

        public int Id { get; set; }

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
            OptionPressed?.Invoke(Id);
        }
    }
}