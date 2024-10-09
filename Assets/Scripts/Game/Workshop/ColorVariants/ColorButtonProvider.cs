using Core.Navigation;
using Game.Workshop.UI;
using LevelEditing.UI;
using UI.Screens;

namespace LevelEditor.ColorVariants
{
    public class ColorButtonProvider : IColorButtonProvider
    {
        public ColorButtonProvider(INavigationService navigationService)
        {
            ColorButton = navigationService.GetScreen<EditLevelScreen>().ColorButton;       
        }
        
        public ColorButton ColorButton { get; }
    }
}