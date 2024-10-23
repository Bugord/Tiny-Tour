using Common.Road;
using Game.Workshop.Core;
using Game.Workshop.Editing.Core;
using Game.Workshop.WorkshopState.Core;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor", menuName = "Installers/Level Editor/Level Editor Installer")]
    public class WorkshopInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WorkshopEntryPoint>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<WorkshopStateMachine>().AsSingle();
            Container.BindInterfacesTo<WorkshopStateFactory>().AsTransient();

            Container.BindInterfacesTo<WorkshopEditorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WorkshopLevelEditorController>().AsSingle();
        }
    }
}