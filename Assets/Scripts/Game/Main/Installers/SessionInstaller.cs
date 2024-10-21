using Game.Main.Session.Core;
using UnityEngine;
using Zenject;

namespace Game.Main.Installers
{
    [CreateAssetMenu(fileName = "installer_session", menuName = "Installers/Main/Session")]
    public class SessionInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<SessionManger>().AsCached();
        }
    }
}