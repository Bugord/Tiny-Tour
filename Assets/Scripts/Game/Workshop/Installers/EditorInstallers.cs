using Common.Editors.Logistic;
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
            Container.BindInterfacesTo<EditorServiceRoadEditor>().AsSingle();
            Container.Bind<ITerrainLevelEditor>().To<TerrainLevelEditor>().AsSingle();
            Container.Bind<ISpawnPointEditor>().To<SpawnPointEditor>().AsSingle();
            Container.Bind<IGoalEditor>().To<GoalEditor>().AsSingle();
        }
    }
}