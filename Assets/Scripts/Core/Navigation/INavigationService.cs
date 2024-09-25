using UI.Screens;

namespace Core.Navigation
{
    public interface INavigationService
    {
        void PopLast();
        void PopToRoot();
        T Push<T>() where T : BaseScreen;
        T Replace<T>() where T : BaseScreen;
        void PopScreen(BaseScreen screen);
        T GetScreen<T>() where T : BaseScreen;
    }
}