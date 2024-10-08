using System.Collections.Generic;
using System.Linq;
using Core.Navigation.Core;
using Core.UI;
using UI.Screens;
using UnityEditor.Experimental.GraphView;

namespace Core.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IScreenFactory screenFactory;
        private readonly IPopupFactory popupFactory;
        private readonly UIProvider uiProvider;

        private readonly LinkedList<BaseScreen> screensLinkedList;
        private readonly LinkedList<BasePopup> popupsLinkedList;

        public NavigationService(IScreenFactory screenFactory, IPopupFactory popupFactory, UIProvider uiProvider)
        {
            this.screenFactory = screenFactory;
            this.popupFactory = popupFactory;
            this.uiProvider = uiProvider;

            screensLinkedList = new LinkedList<BaseScreen>();
            popupsLinkedList = new LinkedList<BasePopup>();
        }

        public void PopLastScreen()
        {
            var lastScreen = screensLinkedList.Last();

            lastScreen.Destroy();
            screensLinkedList.Remove(lastScreen);

            if (screensLinkedList.Count != 0) {
                screensLinkedList.Last().SetActive();
            }
        }

        public void PopScreensToRoot()
        {
            while (screensLinkedList.Count > 1) {
                PopLastScreen();
            }
        }

        public T PushScreen<T>() where T : BaseScreen
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

        public T ReplaceScreen<T>() where T : BaseScreen
        {
            PopLastScreen();
            return PushScreen<T>();
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

        public T PushPopup<T>() where T : BasePopup
        {
            var newPopup = popupFactory.Create<T>(uiProvider.Canvas.transform);
            if (newPopup == null) {
                return null;
            }

            var newPopupPriority = newPopup.Priority;

            if (popupsLinkedList.Count != 0) {
                var previousPriorityPopup = popupsLinkedList.FirstOrDefault(popup => popup.Priority <= newPopupPriority);
                if (previousPriorityPopup != null) {
                    var previousPriorityPopupNode = popupsLinkedList.Find(previousPriorityPopup);
                    if (popupsLinkedList.Last == previousPriorityPopupNode) {
                        previousPriorityPopup.SetInactive();
                    }
                    popupsLinkedList.AddAfter(previousPriorityPopupNode, newPopup);
                }
                else {
                    popupsLinkedList.AddLast(newPopup);
                }
            }
            else {
                popupsLinkedList.AddLast(newPopup);
            }
            return newPopup;
        }

        public void ClosePopup(BasePopup popup)
        {
            var isLast = popupsLinkedList.Last() == popup;

            popupsLinkedList.Remove(popup);
            popup.Destroy();

            if (isLast && popupsLinkedList.Count != 0) {
                popupsLinkedList.Last().SetActive();
            }
        }

        public void CloseAllPopups()
        {
            foreach (var popup in popupsLinkedList) {
                popup.Destroy();
            }
            
            popupsLinkedList.Clear();
        }

        public T GetPopup<T>() where T : BasePopup
        {
            foreach (var popup in popupsLinkedList) {
                if (popup is T searchedPopup) {
                    return searchedPopup;
                }
            }

            return null;
        }
    }
}