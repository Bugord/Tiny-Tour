using Core.Logging;
using UnityEngine;
using Zenject;

namespace Game.Main.Installers.Project
{
    [CreateAssetMenu(fileName = "installer_logger", menuName = "Installers/Project/Logger")]
    public class LoggerInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind(typeof(ILogger<>)).To(typeof(Logger<>)).AsTransient();
        }
    }
}