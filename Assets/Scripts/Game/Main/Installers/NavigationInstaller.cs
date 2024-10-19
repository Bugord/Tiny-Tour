using Core.Navigation;
using Game.Common.Editors.Options.Core;
using Game.Common.UI.Editing.EditorOption;
using Game.Navigation.Systems;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "installer_navigation", menuName = "Installers/Navigation")]
    public class NavigationInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private UIProviderConfig uiProviderConfig;

        [SerializeField]
        private NavigationConfig navigationConfig;

        [SerializeField]
        private EditorOptionUI editorOptionUIPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EditorOptionUIFactory>().AsTransient().WithArguments(editorOptionUIPrefab);
            Container.BindInterfacesTo<NavigationService>().AsSingle();
            Container.BindInterfacesTo<ScreenFactory>().AsTransient();
            Container.BindInterfacesTo<PopupFactory>().AsTransient();
            Container.Bind<UIProvider>().AsSingle();
            Container.Bind<UIProviderConfig>().FromInstance(uiProviderConfig);
            Container.Bind<NavigationConfig>().FromInstance(navigationConfig);
        }
    }
}