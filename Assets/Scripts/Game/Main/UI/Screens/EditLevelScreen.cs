using System;
using Core.Navigation;
using Game.Common.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Main.UI.Screens
{
    public class EditLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        public event Action SavePressed;
        public event Action PlayPressed;
        public event Action ResetPressed;
        public event Action<float> CameraScaleChanged;

        [field: SerializeField]
        public EditorOptionsControllerUI EditorOptionsControllerUI { get; private set; }

        public void OnBackButtonPressed()
        {
            BackPressed?.Invoke();
        }

        public void OnSaveButtonPressed()
        {
            SavePressed?.Invoke();
        }

        public void OnPlayButtonPressed()
        {
            PlayPressed?.Invoke();
        }

        public void OnResetButtonPressed()
        {
            ResetPressed?.Invoke();
        }

        public void OnCameraScaleSliderValueChanged(Slider slider)
        {
            CameraScaleChanged?.Invoke(slider.value);
        }
    }
}