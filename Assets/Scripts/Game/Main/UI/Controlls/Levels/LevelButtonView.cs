using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class LevelButtonView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text buttonText;

        private Action<LevelButtonView> pressedCallback;

        public void Init(string text, Action<LevelButtonView> pressedCallback)
        {
            this.pressedCallback = pressedCallback;
            buttonText.text = text;
        }

        public void OnButtonPressed()
        {
            pressedCallback?.Invoke(this);
        }
    }
}