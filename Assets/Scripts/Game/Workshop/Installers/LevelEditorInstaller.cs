using Common.Road;
using Game.Workshop.Level.Core;
using LevelEditing.Editing.Core;
using LevelEditing.EditorState.Core;
using LevelEditor;
using LevelEditor.ColorVariants;
using LevelEditor.Level.Core;
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

            Container.BindInterfacesTo<ColorButtonProvider>().AsSingle();

            //todo: move to common installer
            Container.BindInterfacesTo<RoadService>().AsSingle();
        }
    }
}