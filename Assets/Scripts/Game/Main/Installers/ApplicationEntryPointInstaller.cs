using Core;
using Game.Main.Workshop;
using UnityEngine;
using Zenject;

namespace Game.Main.Installers
{
    [CreateAssetMenu(fileName = "installer_application_entry_point", menuName = "Installers/Main/Application Entry Point")]
    public class ApplicationEntryPointInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ApplicationEntryPoint>().AsSingle();
            Container.BindInitializableExecutionOrder<ApplicationEntryPoint>(20);

            Container.BindInterfacesTo<WorkshopService>().AsCached();
        }
    }
}