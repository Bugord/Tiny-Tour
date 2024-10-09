using System;
using Common.UI;
using Core.Navigation;
using Game.Workshop.UI;
using LevelEditing.UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EditLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        public event Action SavePressed;
        public event Action PlayPressed;
        public event Action<float> CameraScaleChanged;

        [field: SerializeField]
        public EditorControllerUI EditorControllerUI { get; private set; }
        
        [field: SerializeField]
        public EditorOptionsControllerUI EditorOptionsControllerUI { get; private set; }        
        
        [field: SerializeField]
        public ColorButton ColorButton { get; private set; }

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

        public void OnCameraScaleSliderValueChanged(Slider slider)
        {
            CameraScaleChanged?.Invoke(slider.value);
        }
    }
}