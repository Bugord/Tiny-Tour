using Core.Logging;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "installer_logger", menuName = "Installers/Logger")]
    public class LoggerInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ILogger<>)).To(typeof(Logger<>)).AsTransient();
        }
    }
}