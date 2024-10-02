using Common.Editors.Terrain;
using LevelEditor.EditorState.Core;
using UnityEngine;
using Zenject;

namespace LevelEditor.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor", menuName = "Installers/Level Editor/Level Editor Installer")]
    public class LevelEditorInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelEditorEntryPoint>().AsSingle().NonLazy();
            
            Container.Bind<EditorStateMachine>().AsSingle();
            Container.BindInterfacesTo<EditorStateFactory>().AsTransient();
        }
    }
}