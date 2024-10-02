using Common.Editors.Road;
using Common.Editors.Terrain;
using LevelEditing.Editing.Editors;
using UnityEngine;
using Zenject;

namespace LevelEditing.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor_editors", menuName = "Installers/Level Editor/Editors")]
    public class EditorInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRoadEditor>().To<EditorRoadEditor>().AsSingle();
            Container.Bind<ITerrainEditor>().To<TerrainEditor>().AsSingle();
        }
    }
}