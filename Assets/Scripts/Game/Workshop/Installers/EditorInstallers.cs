using Common.Editors;
using Common.Editors.Obstacles;
using Common.Editors.Terrain;
using Game.Common.Editors.Goals;
using Game.Workshop.Editing.Editors;
using Game.Workshop.LevelEditor.Editors;
using UnityEngine;
using Zenject;

namespace Game.Workshop.Installers
{
    [CreateAssetMenu(fileName = "installer_level_editor_editors", menuName = "Installers/Level Editor/Editors")]
    public class EditorInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WorkshopRoadLevelLevelEditor>().AsSingle();
            Container.Bind<ITerrainLevelEditor>().To<TerrainLevelEditor>().AsSingle();
            Container.Bind<IObstaclesEditor>().To<ObstaclesEditor>().AsSingle();
            Container.Bind<ISpawnPointLevelEditor>().To<SpawnPointLevelEditor>().AsSingle();
            Container.Bind<IGoalLevelEditor>().To<GoalLevelEditor>().AsSingle();
        }
    }
}