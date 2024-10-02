using System.Collections.Generic;
using System.Linq;
using Core.UI;
using UI.Screens;

namespace Core.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IScreenFactory screenFactory;
        private readonly UIProvider uiProvider;
        
        private readonly LinkedList<BaseScreen> screensLinkedList;
        
        public NavigationService(IScreenFactory screenFactory, UIProvider uiProvider)
        {
            this.screenFactory = screenFactory;
            this.uiProvider = uiProvider;

            screensLinkedList = new LinkedList<BaseScreen>();
        }

        public void PopLast()
        {
            var lastScreen = screensLinkedList.Last();

            lastScreen.Destroy();
            screensLinkedList.Remove(lastScreen);

            if (screensLinkedList.Count != 0) {
                screensLinkedList.Last().SetActive();
            }
        }

        public void PopToRoot()
        {
            while (screensLinkedList.Count > 1) {
                PopLast();
            }
        }

        public T Push<T>() where T : BaseScreen
        {
            var screen = screenFactory.Create<T>(uiProvider.Canvas.transform);
            if (screen == null) {
                return null;
            }

            if (screensLinkedList.Count != 0) {
                screensLinkedList.Last().SetInactive();
            }

            screensLinkedList.AddLast(screen);
            return screen;
        }

        public T Replace<T>() where T : BaseScreen
        {
            PopLast();
            return Push<T>();
        }

        public void PopScreen(BaseScreen screen)
        {
            var isLast = screensLinkedList.Last() == screen;

            screensLinkedList.Remove(screen);
            screen.Destroy();

            if (isLast && screensLinkedList.Count != 0) {
                screensLinkedList.Last().SetActive();
            }
        }

        public T GetScreen<T>() where T : BaseScreen
        {
            foreach (var screen in screensLinkedList) {
                if (screen is T typedScreen) {
                    return typedScreen;
                }
            }

            return null;
        }
    }
}