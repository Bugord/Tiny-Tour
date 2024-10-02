using Common.Editors.Options.Core;
using Gameplay.Editing.Options.Data;
using UnityEngine;
using Zenject;

namespace Common.Installers
{
    [CreateAssetMenu(fileName = "installer_common_editor_options", menuName = "Installers/Common/Editor Options")]
    public class EditorOptionsInstaller : ScriptableObjectInstaller
    {
        [SerializeField]
        private EditorOptionDataLibrary editorOptionDataLibrary;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EditorOptionFactory>().AsTransient();
            Container.Bind<EditorOptionDataLibrary>().FromInstance(editorOptionDataLibrary);
        }
    }
}