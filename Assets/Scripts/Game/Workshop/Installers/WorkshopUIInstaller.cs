using Game.Workshop.UI;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Installers
{
    [CreateAssetMenu(fileName = "installer_workshop_ui_provider", menuName = "Installers/Level Editor/Workshop UI Provider")]
    public class WorkshopUIInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WorkshopUIProvider>().AsCached();
            Container.BindInterfacesTo<WorkshopEditorOptionsControllerUIProvider>().AsCached();
        }
    }
}