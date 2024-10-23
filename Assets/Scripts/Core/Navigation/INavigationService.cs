namespace Core.Navigation
{
    public interface INavigationService
    {
        void PopLastScreen();
        void PopScreensToRoot();
        T PushScreen<T>() where T : BaseScreen;
        T ReplaceScreen<T>() where T : BaseScreen;
        void PopScreen(BaseScreen screen);
        T GetScreen<T>() where T : BaseScreen;
        T PushPopup<T>() where T : BasePopup;
        void ClosePopup(BasePopup popup);
        void CloseAllPopups();
        T GetPopup<T>() where T : BasePopup;
    }
}