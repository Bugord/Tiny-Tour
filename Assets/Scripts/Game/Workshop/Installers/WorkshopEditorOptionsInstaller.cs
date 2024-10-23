using Game.Common.Installers;
using Game.Workshop.Editing.Editors;
using Game.Workshop.UI;
using UnityEngine;

namespace Game.Workshop.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor_options", menuName = "Installers/Level Editor/Editors")]
    public class WorkshopEditorOptionsInstaller : BaseEditorOptionsInstaller
    {
        public override void InstallBindings()
        {
            base.InstallBindings();
            
            Container.BindInterfacesTo<WorkshopRoadEditor>().AsSingle();
            Container.BindInterfacesTo<SpawnPointEditor>().AsSingle();
        }
    }
}