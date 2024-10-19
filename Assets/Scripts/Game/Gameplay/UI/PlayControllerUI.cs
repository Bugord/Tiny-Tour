using System;
using Game.Common.UI;
using UnityEngine;

namespace Gameplay.UI
{
    public class PlayControllerUI : MonoBehaviour
    {
        public event Action PlayToggledOn;
        public event Action PlayToggledOff;
        public event Action ResetPressed;

        [SerializeField]
        private SwitchableToggle playToggle;

        private void OnEnable()
        {
            playToggle.ValueChanged += OnPlayToggle;
        }

        private void OnDisable()
        {
            playToggle.ValueChanged -= OnPlayToggle;
        }

        public void TogglePlaySilently(bool isOn)
        {
            playToggle.SetIsOnWithoutNotify(isOn);
        }

        public void OnResetPressed()
        {
            ResetPressed?.Invoke();
        }

        private void OnPlayToggle(bool inOn)
        {
            if (inOn) {
                PlayToggledOn?.Invoke();
            }
            else {
                PlayToggledOff?.Invoke();
            }
        }
    }
}