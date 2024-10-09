using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "installer_application_entry_point", menuName = "Installers/Application Entry Point")]
    public class ApplicationEntryPointInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ApplicationEntryPoint>().AsSingle();
            Container.BindInitializableExecutionOrder<ApplicationEntryPoint>(20);
        }
    }
}