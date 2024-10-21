using Game.Common.EditorOptions;
using Game.Common.Installers;
using Game.Gameplay.Editing.Editors;
using UnityEngine;

namespace Game.Gameplay.Installers
{
    [CreateAssetMenu(fileName = "installer_play_editor_options", menuName = "Installers/Play/Play Editor Options")]
    public class PlayEditorOptionsInstaller : BaseEditorOptionsInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInterfacesTo<PlayRoadEditor>().AsCached();
            Container.BindInterfacesTo<GameplayEditorOptionsControllerUIProvider>().AsCached();
        }
    }
}