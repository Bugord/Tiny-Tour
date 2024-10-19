using Common.Road;
using Game.Common.EditorOptions;
using Game.Workshop.Editing.Core;
using Game.Workshop.Level.Core;
using LevelEditing.EditorState.Core;
using LevelEditor;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor", menuName = "Installers/Level Editor/Level Editor Installer")]
    public class LevelEditorInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WorkshopEntryPoint>().AsSingle().NonLazy();
            
            Container.Bind<EditorStateMachine>().AsSingle();
            Container.BindInterfacesTo<EditorStateFactory>().AsTransient();

            Container.BindInterfacesTo<WorkshopService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelEditorController>().AsSingle();
            
            //todo: move to common installer
            Container.BindInterfacesTo<RoadService>().AsSingle();

            Container.BindInterfacesTo<WorkshopEditorOptionsControllerUIProvider>().AsCached();
        }
    }
}