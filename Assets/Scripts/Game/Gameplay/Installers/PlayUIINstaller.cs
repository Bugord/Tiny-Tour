using Game.Gameplay.UI;
using UnityEngine;
using Zenject;

namespace Game.Gameplay.Installers
{
    [CreateAssetMenu(fileName = "installer_play_ui_provider", menuName = "Installers/Play/Play UI Provider")]
    public class PlayUIInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<PlayUIProvider>().AsCached();
            Container.BindInterfacesTo<GameplayEditorOptionsControllerUIProvider>().AsCached();
        }
    }
}