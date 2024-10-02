using Common.Editors.Options.Core;
using Common.Road;
using LevelEditing.Editing.Core;
using LevelEditing.EditorState.Core;
using LevelEditor;
using LevelEditor.Level.Core;
using UnityEngine;
using Zenject;

namespace LevelEditing.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor", menuName = "Installers/Level Editor/Level Editor Installer")]
    public class LevelEditorInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelEditorEntryPoint>().AsSingle().NonLazy();
            
            Container.Bind<EditorStateMachine>().AsSingle();
            Container.BindInterfacesTo<EditorStateFactory>().AsTransient();

            Container.BindInterfacesTo<LevelEditorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelEditorController>().AsSingle();

            //todo: move to common installer
            Container.BindInterfacesTo<RoadService>().AsSingle();
        }
    }
}