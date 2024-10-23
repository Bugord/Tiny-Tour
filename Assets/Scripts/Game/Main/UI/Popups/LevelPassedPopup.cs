using Core.Navigation;

namespace Game.Main.UI.Popups
{
    public class LevelPassedPopup : BaseModalPopup<bool>
    {
        public void OnContinuePressed()
        {
            SetResult(true);
        }
    }
}