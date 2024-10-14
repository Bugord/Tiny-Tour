using Game.Common.Editors.Options.Core;
using Game.Common.UI.Editing.EditorOption;
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
        
        [SerializeField]
        private EditorOptionUI editorOptionUIPrefab;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<EditorOptionFactory>().AsTransient();
            Container.BindInterfacesTo<EditorOptionUIFactory>().AsTransient().WithArguments(editorOptionUIPrefab);
            Container.Bind<EditorOptionDataLibrary>().FromInstance(editorOptionDataLibrary);
        }
    }
}