using System;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EditLevelScreen : BaseScreen
    {
        public event Action BackPressed;
        public event Action SavePressed;
        public event Action PlayPressed;
        public event Action<float> CameraScaleChanged;

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