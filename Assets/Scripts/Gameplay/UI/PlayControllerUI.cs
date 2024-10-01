using System;
using Common.UI;
using UnityEngine;

namespace Gameplay.UI
{
    public class PlayControllerUI : MonoBehaviour
    {
        public event Action PlayToggledOn;
        public event Action PlayToggledOff;

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

        public void OnPlayToggle(bool inOn)
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