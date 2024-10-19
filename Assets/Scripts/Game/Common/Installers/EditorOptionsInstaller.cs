using Game.Common.EditorOptions;
using Game.Common.Editors.Options.Core;
using Game.Gameplay.Editing.Options.Data;
using UnityEngine;
using Zenject;

namespace Game.Common.Installers
{
    [CreateAssetMenu(fileName = "installer_common_editor_options", menuName = "Installers/Common/Editor Options")]
    public class EditorOptionsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private EditorOptionDataLibrary editorOptionDataLibrary;    
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EditorOptionsController>().AsSingle();
            Container.BindInterfacesTo<EditorOptionFactory>().AsTransient();
            Container.Bind<EditorOptionDataLibrary>().FromInstance(editorOptionDataLibrary);
        }
    }
}