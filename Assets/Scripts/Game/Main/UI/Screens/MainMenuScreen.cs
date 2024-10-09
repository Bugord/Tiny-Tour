using System;
using Core.Navigation;

namespace UI.Screens
{
    public class MainMenuScreen : BaseScreen
    {
        public event Action PlayLevelsPressed;
        public event Action EditLevelsPressed;

        public void OnPlayLevelsButtonPressed()
        {
            PlayLevelsPressed?.Invoke();
        }
        
        public void OnEditLevelsButtonPressed()
        {
            EditLevelsPressed?.Invoke();
        }
    }
}